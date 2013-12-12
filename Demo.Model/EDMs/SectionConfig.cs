using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Demo.Model.DTOs;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.Repository;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using Spring.Context.Support;

namespace Demo.Model.EDMs
{
    public class SectionConfig : AggregateRootBase<SectionConfig, int>
    {
        #region 属性

        #region Id关联

        public int ArticleId { get; set; }

        public int? ParentId { get; set; }

        #endregion

        #region 引用关联

        public virtual InputConfig Input { get; set; }

        public virtual SectionConfig ParentSection { get; set; }

        public virtual ICollection<SectionConfig> SubSections { get; set; }

        #endregion

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }

        /// <summary>
        /// Section名称
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// 深度
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// 排序索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 可否重复添加
        /// </summary>
        public bool IsRepeatable { get; set; }

        /// <summary>
        /// 层级树关系标识(自动生成)
        /// </summary>
        public string TreeFlag { get; set; }

        #endregion

        #region 实例方法

        public void UpsertInputConfig(InputConfigDto dto)
        {
            var input = Mapper.Map<InputConfigDto, InputConfig>(dto);
            if (this.Input == null)
            {
                this.Input = input;
            }
            else
            {
                this.Input.Update(dto); // todo: 验证手工赋值的必要性
            }
        }

        #endregion

        #region 静态方法

        public static int GetSectionConfigMaxId()
        {
            return Repository.Entities.Any() ? Repository.Entities.Max(x => x.Id) : 0;
        }

        public static List<SectionConfigDto> GetSectionConfigDtos(int? articleId = null)
        {
            var condition = ConditionBuilder<SectionConfig>.Create();
            if (articleId != null)
            {
                condition.Equal(x => x.ArticleId, articleId.Value);
            }
            return Repository.Entities
                .Where(condition.Predicate)
                .OrderBy(x => x.Depth)
                .ThenBy(x => x.Index)
                .Project().To<SectionConfigDto>().ToList();
        }

        public static InputConfigDto GetInputConfigDto(int sectionId)
        {
            var section = Repository[sectionId];
            if (section != null)
            {
                return Mapper.Map<InputConfig, InputConfigDto>(section.Input);
            }
            return null;
        }

        public static List<InputConfigDto> GetInputConfigDtos()
        {
            var inputs = Repository.Entities.Where(x => x.Input != null).Select(x => x.Input).ToList();
            return Mapper.Map<List<InputConfig>, List<InputConfigDto>>(inputs);
        }

        //public static void SafeDeleteByArticleId(int articleId)
        //{
        //    var deleteItems = Repository.Entities.Where(x => x.ArticleId == articleId);
        //    var idLst = deleteItems.Select(x => x.Id).ToArray();
        //    Repository.Delete(deleteItems, false);
        //    DataCollection.Delete(x => idLst.Contains(x.SectionId));
        //}

        //public static void SafeDeleteBySectionId(int sectionId)
        //{
        //    Repository.Delete(sectionId, false);
        //    DataCollection.Delete(x => x.SectionId == sectionId);
        //}

        #endregion
    }
}
