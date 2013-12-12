using Demo.Service.IServices;
using Demo.Model.DTOs;
using EasyAccess.Infrastructure.Service;
using EasyAccess.Infrastructure.Util;

namespace Demo.Service.Services
{
    public class FormManageSvc : ServiceBase, IFormManageSvc
    {
        public AritcleSectionInputDataDto GetAritcleSectionInputDataDto(int formId)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult RemoveForm(int formId)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult UpsertForm(FormConfigDto infoDto)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult UpsertFormData(int formId, System.Collections.Generic.List<DataCollectionDto> dtos)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult RemoveFormData(int formId, int groupId, string treeFlag)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult RemoveArticleConfig(int articleId)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult UpsertArticleConfig(ArticleConfigDto dto)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult AddSectionConfig(SectionConfigDto dto)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult UpdateSectionConfig(SectionConfigDto dto)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult RemoveSectionConfig(int sectionId)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult UpsertInputConfig(InputConfigDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}