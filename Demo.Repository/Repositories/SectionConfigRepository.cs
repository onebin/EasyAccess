using System.Linq;
using Demo.Model.EDMs;
using EasyAccess.Infrastructure.Repository;

namespace Demo.Repository.Repositories
{
    public partial class SectionConfigRepository
    {
        public override int Insert(SectionConfig entity, bool isSave = true)
        {
            string treeFlag = "";
            if (entity.ParentId != null)
            {
                treeFlag = Entities.First(x => x.Id == entity.ParentId).TreeFlag;
            }
            entity.TreeFlag = treeFlag + string.Format("{0:D5}", entity.Id);
            return base.Insert(entity, isSave);
        }
    }
}
