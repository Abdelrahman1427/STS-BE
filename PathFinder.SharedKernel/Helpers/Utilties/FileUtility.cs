using PathFinder.DataTransferObjects.Resources;
using PathFinder.SharedKernel.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace PathFinder.SharedKernel.Helpers.Utilties
{
    public static class FileUtility
    {
        public async static Task<string> UploadFile(string directoryPath, string virtualPath, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                var extension = Path.GetExtension(file.FileName);
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                string filePath = Path.Combine(directoryPath, fileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Path.Combine(virtualPath, fileName);
            }
            return null;
        }
        public async static Task<string> UploadFile(string directoryPath, string virtualPath, byte[] file, string fileName)
        {
            fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "__" + fileName;
            string filePath = $"{directoryPath}{virtualPath}{fileName}";
            await File.WriteAllBytesAsync(filePath, file);
            return $"{virtualPath}{fileName}";
        }
        public static void DeleteFilesInDirectory(string directoryPath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(directoryPath);
                File.Delete(directoryPath);
                fileInfo.Delete();
            }
            catch (Exception ex)
            {
                //throw new PathFinderException(CoreResources.RemoveImage);
            }
        }

        public async static Task<string> UploadFile(string directoryPath, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                var extension = Path.GetExtension(file.FileName);
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                string filePath = Path.Combine(directoryPath, fileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return fileName;
            }
            return null;
        }
        public static byte[] ConvertToBytes(IFormFile file)
        {
            Stream stream = file.OpenReadStream();
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static bool CheckFile(IFormFile file)
        {
            bool isPhoto = true;
            var fileExtention = Path.GetExtension(file.FileName);
            List<string> extentions = new List<string>()
            {
                ".png",".jpg",".jpeg",".bmp",".webp"
            };
            if (!extentions.Contains(fileExtention))
                isPhoto = false;
            return isPhoto;
        }
        public static string GetImageAsBase64(string url)
        {
            byte[] bytes;
            using (var client = new WebClient())
            {
                bytes = client.DownloadData(url);
            }
            var base64String = Convert.ToBase64String(bytes);
            return base64String;
        }

        public static byte[] GetImageAsArrayByte(string url)
        {
            byte[] bytes;
            using (var client = new WebClient())
            {
                bytes = client.DownloadData(url);
            }
            return bytes;
        }

        public static bool IsImageFile(string fileName)
        {
            var imageExtensions = new[] { ".jpg", ".jpeg", ".png" }; // Add more image extensions if needed
            var extension = Path.GetExtension(fileName);
            return imageExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }
    }
}
