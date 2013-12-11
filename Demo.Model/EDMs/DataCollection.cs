using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Demo.Model.DTOs;
using EasyAccess.Infrastructure.Entity;

namespace Demo.Model.EDMs
{
    public class DataCollection : AggregateRootBase<DataCollection, int>
    {
        public int FormId { get; set; }

        public int SectionId { get; set; }

        public int GroupId { get; set; }

        [MaxLength(255)]
        public string Value { get; set; }

        #region 静态方法

        public static List<DataCollectionDto> GetSupplierDataCollectionDtos(int formId)
        {
            var datas = Repository.Entities.Where(x => x.FormId == formId);
            if (datas.Any())
            {
                return Mapper.Map<List<DataCollection>, List<DataCollectionDto>>(datas.ToList());
            }
            return new List<DataCollectionDto>();
        }

        #endregion
    }
}
