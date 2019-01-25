using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileUpload
{
    /// <summary>
    /// FileUpload1 的摘要说明
    /// </summary>
    public class FileUpload1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string flag = context.Request["flag"];

            switch (flag)
            {
                case "test111":
                    Upload(context);
                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void Upload(HttpContext context)
        {
            var Request = context.Request;
            var fileKeys = Request.Files.Keys;

            string templateDirectory = context.Server.MapPath("~/Template");
            if (!Directory.Exists(templateDirectory))
            {
                Directory.CreateDirectory(templateDirectory);
            }

            IDictionary<string, object> ddlDic = new Dictionary<string, object>();
            for (int i = 0; i < fileKeys.Count; i++)
            {
                string fileName = fileKeys.Get(i);
                var file = Request.Files[fileName];

                string saveFileName = Path.Combine(templateDirectory, Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName));
                file.SaveAs(saveFileName);


                string ddlValue = string.Empty;

                if (fileName.IndexOf("_") > 0)
                {
                    var arr = fileName.Split('_');

                    if (arr != null && arr.Length == 2)
                    {
                        string rowIndex = arr[1];
                        string ddlName = "ddl_" + rowIndex;

                        ddlValue = Request[ddlName];

                        ddlDic.Add(ddlName, ddlValue);
                    }
                }

            }

            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(ddlDic);
            context.Response.Write(result);
        }
    }
}