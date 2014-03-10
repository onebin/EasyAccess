using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.Extensions;
using EasyAccess.Infrastructure.Util;
using EasyAccess.Infrastructure.Util.CustomTimestamp;
using EasyAccess.Infrastructure.Util.CustomTimestamp.Dialect;
using Spring.Objects.Factory;
using SqlCommandBuilder = EasyAccess.Infrastructure.Util.CustomTimestamp.SqlCommandBuilder;

namespace EasyAccess.Infrastructure.UnitOfWork
{
    public abstract class UnitOfWorkContextBase : IUnitOfWorkContext
    {
        protected abstract DbContext DbContext { get; }

        public DbSet<TEntity> Set<TEntity>()
            where TEntity : class, IAggregateRoot
        {
            return DbContext.Set<TEntity>();
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class, IAggregateRoot
        {
            return DbContext.Entry(entity);
        }

        public void RegisterNew<TEntity>(TEntity entity)
            where TEntity : class, IAggregateRoot
        {
            if (IsRollback) return;
            var state = DbContext.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                DbContext.Entry(entity).State = EntityState.Added;
            }
            IsCommitted = false;
        }

        public void RegisterNew<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IAggregateRoot
        {
            if (IsRollback) return;
            try
            {
                DbContext.Configuration.AutoDetectChangesEnabled = false;
                foreach (var entity in entities)
                {
                    RegisterNew(entity);
                }
            }
            finally
            {
                DbContext.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public void RegisterModified<TEntity>(params TEntity[] entities)
            where TEntity : class, IAggregateRoot
        {
            if (IsRollback) return;
            var dbSet = DbContext.Set<TEntity>();
            foreach (var entity in entities)
            {
                try
                {
                    var entry = DbContext.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        dbSet.Attach(entity);
                        entry.State = EntityState.Modified;
                    }
                }
                catch (InvalidOperationException)
                {
                    var oldEntity = dbSet.Find(entity.Id);
                    DbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
            IsCommitted = false;
        }

        public void RegisterModified<TEntity>(Expression<Func<TEntity, object>> propertyExpression, params TEntity[] entities)
            where TEntity : class, IAggregateRoot
        {
            if (IsRollback) return;
            ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)propertyExpression.Body).Members;
            var dbSet = DbContext.Set<TEntity>();
            foreach (var entity in entities)
            {
                try
                {
                    var entry = DbContext.Entry(entity);
                    entry.State = EntityState.Unchanged;
                    foreach (var memberInfo in memberInfos)
                    {
                        entry.Property(memberInfo.Name).IsModified = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    var originalEntity = dbSet.Local.Single(m => Equals(m.Id, entity.Id));
                    var objectContext = ((IObjectContextAdapter)DbContext).ObjectContext;
                    var objectEntry = objectContext.ObjectStateManager.GetObjectStateEntry(originalEntity);
                    objectEntry.ApplyCurrentValues(entity);
                    objectEntry.ChangeState(EntityState.Unchanged);
                    foreach (var memberInfo in memberInfos)
                    {
                        objectEntry.SetModifiedProperty(memberInfo.Name);
                    }
                }
            }
        }

        public void RegisterDeleted<TEntity>(TEntity entity)
            where TEntity : class, IAggregateRoot
        {
            if (IsRollback) return;
            var entry = DbContext.Entry(entity);
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                entry.Property("IsDeleted").CurrentValue = true;
            }
            else
            {
                entry.State = EntityState.Deleted;
            }
            IsCommitted = false;
        }

        public void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IAggregateRoot
        {
            if (IsRollback) return;
            try
            {
                DbContext.Configuration.AutoDetectChangesEnabled = false;
                foreach (var entity in entities)
                {
                    RegisterDeleted(entity);
                }
            }
            finally
            {
                DbContext.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public bool IsCommitted { get; private set; }
        public bool IsRollback { get; private set; }

        public int Commit(bool force = false)
        {
            if (!force && (IsCommitted || IsRollback))
            {
                return 0;
            }
            try
            {
                var result = SaveChanges() + DbContext.SaveChanges();
                IsCommitted = true;
                return result;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    var sqlEx = e.InnerException.InnerException as SqlException;
                    var msg = DbHelper.GetSqlExceptionMessage(sqlEx.Number);
                    throw new DbUpdateException(msg, sqlEx);
                }
                throw;
            }
        }

        private int SaveChanges()
        {
            var result = 0;
            var commands = new List<string>();
            var reloadItems = new List<DbEntityEntry>();
            foreach (var entry in DbContext.ChangeTracker.Entries())
            {

                if (entry.State != EntityState.Modified) continue;

                var updateCommand = SqlCommandBuilder.Build(entry).Update();

                if (!string.IsNullOrWhiteSpace(updateCommand))
                {
                    commands.Add(updateCommand);
                    reloadItems.Add(entry);
                    entry.State = EntityState.Unchanged;
                }
            }
            if (commands.Count > 0)
            {
                using (var tran = DbContext.Database.BeginTransaction())
                {
                    result += DbContext.Database.ExecuteSqlCommand(string.Join(" ", commands));
                    tran.Commit();
                }
                foreach (var dbEntityEntry in reloadItems)
                {
                    dbEntityEntry.Reload();
                }
            }
            return result;
        }

        public void Rollback()
        {
            IsCommitted = false;
            IsRollback = true;
        }

        public void Dispose()
        {
            if (!IsCommitted && !IsRollback)
            {
                Commit();
            }
            DbContext.Dispose();
        }
    }
}
