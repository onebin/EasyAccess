using System.Collections.Generic;
using Demo.Model.DTOs;
using EasyAccess.Infrastructure.Util;

namespace Demo.Service.IServices
{
    public interface IFormManageSvc
    {
        #region Form

        AritcleSectionInputDataDto GetAritcleSectionInputDataDto(int formId);

        OperationResult RemoveForm(int formId);

        OperationResult UpsertForm(FormConfigDto infoDto);

        OperationResult UpsertFormData(int formId, List<DataCollectionDto> dtos);

        OperationResult RemoveFormData(int formId, int groupId, string treeFlag);

        #endregion

        #region ArticleConfig

        OperationResult RemoveArticleConfig(int articleId);

        OperationResult UpsertArticleConfig(ArticleConfigDto dto);

        #endregion

        #region SectionConfig

        OperationResult AddSectionConfig(SectionConfigDto dto);

        OperationResult UpdateSectionConfig(SectionConfigDto dto);

        OperationResult RemoveSectionConfig(int sectionId);

        #endregion

        #region InputConfig

        OperationResult UpsertInputConfig(InputConfigDto dto);

        #endregion
    }
}