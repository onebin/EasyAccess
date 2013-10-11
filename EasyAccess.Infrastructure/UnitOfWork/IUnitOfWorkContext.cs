﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using EasyAccess.Infrastructure.Entity;

namespace EasyAccess.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkContext: IUnitOfWork, IDisposable
    {
        /// <summary>
        ///   为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        DbSet<TEntity> Set<TEntity, TKey>()
            where TEntity : class, IEntity<TKey>;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        DbEntityEntry<TEntity> Entry<TEntity, TKey>(TEntity entity)
            where TEntity : class, IEntity<TKey>;

        /// <summary>
        ///   注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterNew<TEntity, TKey>(TEntity entity)
            where TEntity : class, IEntity<TKey>;

        /// <summary>
        ///   批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        void RegisterNew<TEntity, TKey>(IEnumerable<TEntity> entities)
            where TEntity : class, IEntity<TKey>;

        /// <summary>
        ///   注册一个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entities"> 要注册的对象 </param>
        void RegisterModified<TEntity, TKey>(params TEntity[] entities)
            where TEntity : class, IEntity<TKey>;

        /// <summary>
        /// 使用指定的属性表达式指定注册更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity">要注册的类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="propertyExpression">属性表达式，包含要更新的实体属性</param>
        /// <param name="entities">附带新值的实体信息，必须包含主键</param>
        void RegisterModified<TEntity, TKey>(Expression<Func<TEntity, object>> propertyExpression,
            params TEntity[] entities)
            where TEntity : class, IEntity<TKey>;

        /// <summary>
        ///   注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterDeleted<TEntity, TKey>(TEntity entity)
            where TEntity : class, IEntity<TKey>;

        /// <summary>
        ///   批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        void RegisterDeleted<TEntity, TKey>(IEnumerable<TEntity> entities)
            where TEntity : class, IEntity<TKey>;
    }
}
