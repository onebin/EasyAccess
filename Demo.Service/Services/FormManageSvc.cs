using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Demo.Model.EDMs;
using Demo.Service.IServices;
using Demo.Model.DTOs;
using EasyAccess.Infrastructure.Constant;
using EasyAccess.Infrastructure.Service;
using EasyAccess.Infrastructure.Util;

namespace Demo.Service.Services
{
    public class FormManageSvc : ServiceBase, IFormManageSvc
    {
        public AritcleSectionInputDataDto GetAritcleSectionInputDataDto(int formId)
        {
            var dto = new AritcleSectionInputDataDto
            {
                Articles = ArticleConfig.GetArticleConfigDtos(),
                Sections = SectionConfig.GetSectionConfigDtos(),
                Inputs = SectionConfig.GetInputConfigDtos(),
                Datas = DataCollection.GetDataCollectionDtos(formId)
            };
            return dto;
        }

        public OperationResult RemoveForm(int formId)
        {
            var supplier = FormConfig.FindById(formId);
            if (supplier == null)
            {
                return new OperationResult(StatusCode.NotFound);
            }

            FormConfig.Delete(supplier);
            return UnitOfWork.Commit() > 0
                ? new OperationResult(StatusCode.Okey)
                : new OperationResult(StatusCode.Failed);
        }

        public OperationResult UpsertForm(FormConfigDto formConfig)
        {
            var form = Mapper.Map<FormConfigDto, FormConfig>(formConfig);
            if (form.Id == 0)
            {
                FormConfig.Insert(form);
                return UnitOfWork.Commit() > 0
                    ? new OperationResult(StatusCode.Okey).SetExtras(form.Id)
                    : new OperationResult(StatusCode.Failed);
            }
            else
            {
                FormConfig.Update(form);
                return UnitOfWork.Commit() > 0
                           ? new OperationResult(StatusCode.Okey).SetExtras(form.Id)
                           : new OperationResult(StatusCode.Failed);
            }
        }

        public OperationResult UpsertFormData(int formId, List<DataCollectionDto> dtos)
        {
            var form = FormConfig.FindById(formId);
            if (form != null)
            {
                var formDatas = DataCollection.Find(x => x.FormId == formId);
                foreach (var dto in dtos)
                {
                    var formData = formDatas.FirstOrDefault(x => x.GroupId == dto.GroupId && x.SectionId == dto.SectionId);
                    if (formData != null)
                    {
                        if (formData.Value != dto.Value)
                        {
                            formData.Value = dto.Value;
                        }
                    }
                    else
                    {
                        dto.FormId = formId;
                        var addItem = Mapper.Map<DataCollectionDto, DataCollection>(dto);
                        DataCollection.Insert(addItem);
                    }
                }
                return UnitOfWork.Commit() > 0
                               ? new OperationResult(StatusCode.Okey)
                               : new OperationResult(StatusCode.Failed);
            }
            else
            {
                return new OperationResult(StatusCode.Failed);
            }
        }

        public OperationResult RemoveFormData(int formId, int groupId, string treeFlag)
        {
            var sectionIdLst = SectionConfig.Find(x => x.TreeFlag.StartsWith(treeFlag)).Select(x => x.Id).ToArray();
            DataCollection.Delete(x => x.FormId == formId && x.GroupId == groupId && sectionIdLst.Contains(x.SectionId));
            UnitOfWork.Commit();
            return new OperationResult(StatusCode.Okey);
        }

        public OperationResult RemoveArticleConfig(int articleId)
        {
            ArticleConfig.Delete(articleId);
            return UnitOfWork.Commit() > 0
                           ? new OperationResult(StatusCode.Okey)
                           : new OperationResult(StatusCode.Okey, "早就被删了呢！");
        }

        public OperationResult UpsertArticleConfig(ArticleConfigDto dto)
        {
            var article = Mapper.Map<ArticleConfigDto, ArticleConfig>(dto);
            if (article.Id == 0)
            {
                ArticleConfig.Insert(article);
                return UnitOfWork.Commit() > 0
                    ? new OperationResult(StatusCode.Okey).SetExtras(article.Id)
                    : new OperationResult(StatusCode.Failed);
            }
            else
            {
                ArticleConfig.Update(article);
                return UnitOfWork.Commit() > 0
                           ? new OperationResult(StatusCode.Okey).SetExtras(article.Id)
                           : new OperationResult(StatusCode.Failed);
            }
        }

        public OperationResult AddSectionConfig(SectionConfigDto dto)
        {
            var article = ArticleConfig.FindById(dto.ArticleId);
            if (article != null)
            {
                var section = Mapper.Map<SectionConfigDto, SectionConfig>(dto);
                section.ArticleId = article.Id;
                SectionConfig.Insert(section);
                return UnitOfWork.Commit() > 0
                    ? new OperationResult(StatusCode.Okey)
                    : new OperationResult(StatusCode.Failed);
            }
            return new OperationResult(StatusCode.NotFound, "失败了耶~刷新一下页面看看，会有惊喜哦");
        }

        public OperationResult UpdateSectionConfig(SectionConfigDto dto)
        {
            var section = SectionConfig.FindById(dto.Id);
            if (section == null)
            {
                return new OperationResult(StatusCode.NotFound);
            }
            else
            {
                section.Name = dto.Name;
                section.Index = dto.Index;
                section.IsRepeatable = dto.IsRepeatable;
                SectionConfig.Update(section);
                return UnitOfWork.Commit() > 0
                           ? new OperationResult(StatusCode.Okey)
                           : new OperationResult(StatusCode.Error, "没有做过任何修改吧？");
            }
        }

        public OperationResult RemoveSectionConfig(int sectionId)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult UpsertInputConfig(InputConfigDto dto)
        {
            var section = SectionConfig.FindById(dto.SectionId);
            if (section == null)
            {
                return new OperationResult(StatusCode.NotFound, "").SetExtras(dto.Id);
            }
            else
            {
                section.UpsertInputConfig(dto);
                return UnitOfWork.Commit() > 0
                           ? new OperationResult(StatusCode.Okey).SetExtras(section.Input.Id)
                           : new OperationResult(StatusCode.Failed).SetExtras(section.Input.Id);
            }
        }
    }
}