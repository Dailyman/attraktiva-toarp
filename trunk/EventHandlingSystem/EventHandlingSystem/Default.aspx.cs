using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using  EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SaveOrRestoreFileUpload();
        }


        protected void ButtonSave_OnClick(object sender, EventArgs e)
        {
            string saveDirPath = HttpContext.Current.Server.MapPath("~/Uploads/Images/");
            string fileName;



            if (!FileUpload.HasFile)
            {
                LabelFileUpload.Text = "Select a file!";
                return;
            }
            if (String.IsNullOrWhiteSpace(FileUpload.PostedFile.FileName))
            {
                LabelFileUpload.Text = "File name was empty!";
                return;
            }
            if (File.Exists(saveDirPath + FileUpload.PostedFile.FileName))
            {
                LabelFileUpload.Text = "File with that name already exists!";
                return;
            }

            fileName = FileUpload.PostedFile.FileName;
            string uploadedFilePath = "";
            try
            {
                //LabelFileUpload.Text += saveDirPath + fileName;
                FileUpload.SaveAs(saveDirPath + fileName);
                uploadedFilePath = saveDirPath + fileName;
            }
            catch (Exception ex)
            {
                LabelFileUpload.Text = string.Format("Error: Unable to save file <br/> {0}", ex.Message);
                return;
            }

            LabelFileUpload.Text =
                string.Format(
                    "<br>File was uploaded to: <br>{0} <br>File name: {1} <br>File type: {2} <br>File length: {3} bytes <br>Adress: {4}",
                    uploadedFilePath, FileUpload.PostedFile.FileName, FileUpload.PostedFile.ContentType,
                    FileUpload.PostedFile.ContentLength, MapPathReverse(uploadedFilePath));

            LinkImage.NavigateUrl = MapPathReverse(uploadedFilePath);
            Image1.ImageUrl = MapPathReverse(uploadedFilePath);

        }

        public static string MapPathReverse(string fullServerPath)
        {
            var myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            string pathQuery = myuri.PathAndQuery;
            string hostName = myuri.ToString().Replace(pathQuery, "");

            String RelativePath = String.Empty;
            RelativePath = fullServerPath.Replace(HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"], String.Empty).Replace("\\", "/");
            //return hostName +"/"+ RelativePath;
            return @"~\"+RelativePath;
            
            return @"~\" + fullServerPath.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty);
        }

        private void SaveOrRestoreFileUpload()
        {
            //If first time page is submitted and we have file in FileUpload control but not in session 
            // Store the values to Session Object 
            if (Session["FileUpload"] == null && FileUpload.HasFile)
            {
                Session["FileUpload"] = FileUpload;
                LabelFileUpload.Text = FileUpload.FileName;
            }
            // Next time submit and Session has values but FileUpload is Blank 
            // Return the values from session to FileUpload 
            else if (Session["FileUpload"] != null && !FileUpload.HasFile)
            {
                FileUpload = (FileUpload)Session["FileUpload"];
                LabelFileUpload.Text = FileUpload.FileName;
            }
            // Now there could be another situation when Session has File but user want to change the file 
            // In this case we have to change the file in session object 
            else if (FileUpload.HasFile)
            {
                Session["FileUpload"] = FileUpload;
                LabelFileUpload.Text = FileUpload.FileName;
            }
        }

    }
}