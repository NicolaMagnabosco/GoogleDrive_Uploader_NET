using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleDriveUploader
{
    class DriveUploader
    {
        private UserCredential credential;
        private DriveService driveService;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="jsonSecretPath"></param>
        /// <param name="appName"></param>
        public DriveUploader(string jsonSecretPath, string appName)
        {
            GetCredential(jsonSecretPath);
            CreateDriveService(appName);
        }

        /// <summary>
        /// Request Google Authentication for the current application with given the 'secret' file.
        /// </summary>
        /// <param name="clientSecretPath"></param>
        private void GetCredential(string clientSecretPath)
        {
            using (var filestream = new FileStream(clientSecretPath,
                FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(filestream).Secrets,
                    new[] { DriveService.Scope.Drive },
                    "user",
                    CancellationToken.None,
                    new FileDataStore("DriveCommandLineSample")).Result;
            }
        }

        /// <summary>
        /// Create a new service for the application
        /// </summary>
        /// <param name="applicationName"></param>
        private void CreateDriveService(string applicationName)
        {
            driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });
        }

        /// <summary>
        /// Upload a file to Google Drive
        /// </summary>
        /// <param name="file">Represents the file in bytes</param>
        /// <param name="title">The name of the file as it will appear on Google Drive</param>
        /// <param name="description">A file description</param>
        /// <param name="mimetype">Type of the file we are going to upload</param>
        private void uploadDocumentToDrive(ref byte[] file, string title, string description, string mimetype)
        {
            Google.Apis.Drive.v2.Data.File body = new Google.Apis.Drive.v2.Data.File();
            body.Title = title;
            body.Description = description;
            body.MimeType = mimetype;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(file))
            {
                if (driveService != null)
                {
                    FilesResource.InsertMediaUpload request = driveService.Files.Insert(body, stream, mimetype);
                    request.Upload();
                    Google.Apis.Drive.v2.Data.File uploadedFile = request.ResponseBody;
                    System.Diagnostics.Debug.WriteLine("Uploaded file: {0} with ID: {1}",
                        uploadedFile.Title,
                        uploadedFile.Id);
                }
            }
        }

        /// <summary>
        /// Public interface method which allows to request an upload for the passed file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="mimetype"></param>
        /// <returns></returns>
        public bool UploadFile(ref byte[] file, string title, string description, string mimetype)
        {
            bool uploaded = false;
            if (credential != null)
            {
                uploadDocumentToDrive(ref file,title,description, mimetype);
                uploaded = true;
            }
            return uploaded;
        }      
    }
}
