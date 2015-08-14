using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace GoogleDriveUploader
{
    public partial class Form1 : Form
    {
        // Change this value with the path where your secret JSON file is
        private static string json_secret_file = @".\client_secret.json";
        private static string application_name = @".NET Google Drive Uploader";

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set the current file to the one chosen in the file dialog
        /// </summary>
        private void button_browse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    textBox_path.Text = openFileDialog1.FileName;
                    textBox_fileName.Text = openFileDialog1.SafeFileName;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// A very simple method that return the MIME file type of a file. Currently it only supports few types.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private String GetFileType(string file)
        {
            string extension = Path.GetExtension(file);
            System.Diagnostics.Debug.WriteLine("extension: " + extension);
            string mime;
            switch (extension.ToLower())
            {
                case ".jpg":
                    mime = "image/jpeg";
                    break;
                case ".jpeg":
                    mime = "image/jpeg";
                    break;
                case ".png":
                    mime = "image/png";
                    break;
                case ".doc":
                    mime = "application/msword";
                    break;
                case ".pdf":
                    mime = "application/pdf";
                    break;
                default:
                    mime = "text/plain";
                    break;
            }
            return mime;
        }

        /// <summary>
        /// Uploads file on Google Drive after user's click
        /// </summary>
        private void button_upload_Click(object sender, EventArgs e)
        {
           
            DriveUploader driveUploader = new DriveUploader(json_secret_file, application_name);
            System.Diagnostics.Debug.WriteLine(textBox_path.Text);
            try
            {
                byte[] byteArray = System.IO.File.ReadAllBytes(textBox_path.Text);

                string filePath = textBox_path.Text;
                string fileName = textBox_fileName.Text;
                string description = textBox_description.Text;
                string fileType = GetFileType(filePath);
             driveUploader.UploadFile(ref byteArray, fileName, description,fileType);
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }

        }
    }
}
