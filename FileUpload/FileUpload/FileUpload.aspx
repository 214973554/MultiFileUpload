<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="FileUpload.FileUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/jquery.form.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div>
            <table id="fileUpload" border="1">
                <tr>
                    <td id="tdOriginal">选择附件：</td>
                    <td>
                        <asp:DropDownList CssClass="ddlOriginal" ID="ddl_1" Name="ddl_1" runat="server"></asp:DropDownList></td>
                    <td>
                        <asp:FileUpload ID="file_1" Name="file_1" runat="server" /></td>
                    <td>
                        <input type="button" class="btnAdd" value="添加" /><asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" /><input type="button" id="btnAjaxUpload" value="ajax上传" /></td>
                </tr>
            </table>
        </div>
    </form>
    <script>
        var rowIndex = 1;
        $(function () {
            $(".btnAdd").on("click", function () {

                rowIndex++;
                var _options = "";
                $(".ddlOriginal option").each(function (i, opt) {
                    _options += '<option value="' + opt.value + '">' + opt.text + '</option>';
                });

                var _tr = '<tr>'
             + '<td><select name="ddl_' + rowIndex + '">'
             + _options
                + '</select></td><td><input type="file" name="file_' + rowIndex + '"/></td><td><input type="button" value="移除" class="btnRemove" /></td>'
         + '</tr>'

                $("#fileUpload").append(_tr);
                var rowCount = $("#fileUpload").find("tr").length;
                $("#tdOriginal").attr("rowspan", rowCount);

            });

            $("#fileUpload").on("click", ".btnRemove", function () {
                $(this).parent().parent().remove();
            });

            $("#btnAjaxUpload").on("click", function () {
                $("#form1").ajaxSubmit({
                    type: "POST",
                    url: "/FileUpload.ashx?flag=test111",
                    data: $("#form1").formSerialize(),
                    dataType:"json",
                    success: function (responseText, statusText) {
                        console.log(responseText);
                        console.log(statusText);
                    }
                });
            });

        });


    </script>
</body>
</html>
