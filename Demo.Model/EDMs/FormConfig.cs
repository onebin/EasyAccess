using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Demo.Model.DTOs;
using EasyAccess.Infrastructure.Entity;

namespace Demo.Model.EDMs
{
    public class FormConfig : AggregateRootBase<FormConfig, int>
    {
        #region 属性

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Memo { get; set; }

        #endregion

        #region 实例方法

        #endregion

        #region 静态方法

        public static List<FormConfigDto> GetDtos()
        {
            return Repository.Entities.Project().To<FormConfigDto>().ToList();
        }

        public static FormConfigDto GetDto(int formId)
        {
            var supplier = Repository[formId];
            var dto = Mapper.Map<FormConfig, FormConfigDto>(supplier);
            return dto;
        }

        #endregion

    }
}
