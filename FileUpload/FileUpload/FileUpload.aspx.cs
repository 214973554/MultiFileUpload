using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileUpload
{
    public partial class FileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDownList();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            var fileKeys = Request.Files.Keys;

            string templateDirectory = Server.MapPath("~/Template");
            if (!Directory.Exists(templateDirectory))
            {
                Directory.CreateDirectory(templateDirectory);
            }

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
                    }
                }

            }

        }

        private void BindDropDownList()
        {
            ddl_1.Items.Add(new ListItem("请选择", ""));
            ddl_1.Items.Add(new ListItem("一", "1"));
            ddl_1.Items.Add(new ListItem("二", "2"));
            ddl_1.Items.Add(new ListItem("三", "3"));
            ddl_1.DataBind();
        }
    }
}