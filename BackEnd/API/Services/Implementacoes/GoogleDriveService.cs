using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace API.Services.Implementacoes
{
    public class GoogleDriveService
    {
        private readonly DriveService _driveService;

        public GoogleDriveService(string credentialsPath)
        {
            var credential = GoogleCredential.FromFile(credentialsPath)
                .CreateScoped(DriveService.Scope.Drive);

            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MinhaAPI"
            });
        }

     
        public async Task<string> CreateFolderAsync(string folderName, string parentFolderId = null)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder",
                Parents = parentFolderId != null ? new List<string> { parentFolderId } : null
            };

            var request = _driveService.Files.Create(fileMetadata);
            request.Fields = "id";
            var folder = await request.ExecuteAsync();
            return folder.Id;
        }


        public async Task<(string FileId, string Url)> UploadFileAsync(IFormFile file, string folderId)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = file.FileName,
                Parents = new List<string> { folderId }
            };

            FilesResource.CreateMediaUpload request;
            await using (var stream = file.OpenReadStream())
            {
                request = _driveService.Files.Create(fileMetadata, stream, GetMimeType(file.FileName));
                request.Fields = "id";
                await request.UploadAsync();
            }

            var fileId = request.ResponseBody.Id;

            // Define permissão pública para visualização
            var permission = new Permission()
            {
                Role = "reader",
                Type = "anyone"
            };
            await _driveService.Permissions.Create(permission, fileId).ExecuteAsync();

          
            var url = $"https://drive.google.com/uc?id={fileId}";

            return (fileId, url);
        }

        private string GetMimeType(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLower();
            return ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
        }
    }
}
