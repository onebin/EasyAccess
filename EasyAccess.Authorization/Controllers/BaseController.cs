using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using EasyAccess.Authorization.Filter;
using EasyAccess.Model.EDMs;
using Microsoft.Reporting.WebForms;

namespace EasyAccess.Authorization.Controllers
{
    [ExceptionFilter]
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前系统登录用户
        /// </summary>
        public Account CurrentAccount
        {
            get { return null; }
        }

        #region 成员变量

        protected int PageIndex = 1;
        protected int PageSize = 15;

        #endregion

        /// <summary>
        /// 初始化基本页面信息
        /// </summary>
        protected void InitPageData(FormCollection formData)
        {
            PageIndex = int.Parse(formData["page"] ?? "1") - 1;
            PageSize = int.Parse(formData["rows"] ?? int.MaxValue.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 导出类型
        /// </summary>
        private class ExportType
        {
            public string ReportType { get; set; }
            public string OutputFormat { get; set; }
            public string ContentType { get; set; }
        }

        /// <summary>
        /// 导出Word报表
        /// </summary>
        /// <param name="dataSet">数据源</param>
        /// <param name="filePath">模板文件路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="reportParameters">参数</param>
        /// <returns>报表</returns>
        public ActionResult ExportWord(DataSet dataSet, string filePath, string fileName, List<ReportParameter> reportParameters = null)
        {
            var exportType = new ExportType()
            {
                ReportType = "Word",
                OutputFormat = "Word",
                ContentType = "application/ms-word"
            };
            return Export(dataSet, filePath, fileName, exportType, reportParameters);
        }

        /// <summary>
        /// 导出Excel报表
        /// </summary>
        /// <param name="dataSet">数据源</param>
        /// <param name="filePath">模板文件路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="reportParameters">参数</param>
        /// <returns>报表</returns>
        public ActionResult ExportExcel(DataSet dataSet, string filePath, string fileName, List<ReportParameter> reportParameters = null)
        {
            var exportType = new ExportType() 
            {
                ReportType = "Excel",
                OutputFormat = "Excel",
                ContentType = "application/ms-excel"
            };
            return Export(dataSet, filePath, fileName, exportType, reportParameters);
        }

        /// <summary>
        /// 报表导出
        /// </summary>
        /// <param name="dataSet">数据源</param>
        /// <param name="filePath">模板文件路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="exportType">导出类型</param>
        /// <param name="reportParameters">参数</param>
        /// <returns>报表</returns>
        private ActionResult Export(DataSet dataSet, string filePath, string fileName, ExportType exportType, List<ReportParameter> reportParameters)
        {
            var localReport = new LocalReport();
            localReport.SetBasePermissionsForSandboxAppDomain(new PermissionSet(PermissionState.Unrestricted));

            localReport.ReportPath = Server.MapPath(filePath);
            foreach (DataTable dt in dataSet.Tables)
            {
                var reportDataSource = new ReportDataSource(dt.TableName, dt);
                localReport.DataSources.Add(reportDataSource);
            }

            if (reportParameters != null && reportParameters.Count > 0)
            {
                foreach (var reportParameter in reportParameters)
                {
                    localReport.SetParameters(reportParameter);
                }
            }

            string reportType = exportType.ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
           "<DeviceInfo>" +
           "  <OutputFormat>" + exportType.OutputFormat + "</OutputFormat>" +
           "  <PageWidth></PageWidth>" +
           "  <PageHeight></PageHeight>" +
           "  <MarginTop></MarginTop>" +
           "  <MarginLeft></MarginLeft>" +
           "  <MarginRight></MarginRight>" +
           "  <MarginBottom></MarginBottom>" +
           "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            Response.Clear();
            Response.ContentType = exportType.ContentType;
            Response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + "." + fileNameExtension);
            Response.BinaryWrite(renderedBytes);
            Response.End();

            return View();
        }
    }
}
