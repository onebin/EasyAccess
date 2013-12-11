using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using Demo.Model.DTOs;
using EasyAccess.Infrastructure.Entity;

namespace Demo.Model.EDMs
{
    public class ArticleConfig : AggregateRootBase<ArticleConfig, int>
    {
        /// <summary>
        /// Article名称
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// 排序索引
        /// </summary>
        public int Index { get; set; }

        #region 静态方法

        #region 重载方法

        #endregion

        public static List<ArticleConfigDto> GetArticleConfigDtos()
        {
            return Repository.Entities.OrderBy(x => x.Index).Project().To<ArticleConfigDto>().ToList();
        }

        #endregion
    }
}
