using PathFinder.DataTransferObjects.DTOs.Document;
namespace PathFinder.WebApp.Services
{
    public interface IDocumentWebService
    {
        Task<List<DocumentRequestDTO>> UploadDocument(List<DocumentTitleDTO> documents, String path);
    }
    public class DocumentWebService : IDocumentWebService
    {
        private readonly IConfiguration _config;
        public DocumentWebService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<List<DocumentRequestDTO>> UploadDocument(List<DocumentTitleDTO> documents, String Folder)
        {
            List<DocumentRequestDTO> documentDTOs = new List<DocumentRequestDTO>();
            foreach (var document in documents)
            {
                if (document.File != null)
                {
                    string uploadsFolder = Path.Combine( _config.GetValue<string>("FileLocation") , Folder);
                    string ImagePath = Guid.NewGuid().ToString() + "_" + document?.FileName;
                    string filePath = Path.Combine(uploadsFolder, ImagePath).Replace(" ", "_");
                    string fullFilePath = Path.Combine("wwwroot", uploadsFolder, ImagePath).Replace(" ", "_");

                    using (FileStream fs = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await document.File.CopyToAsync(fs);
                    }
                    documentDTOs.Add(new DocumentRequestDTO { Path = filePath, Title = document.Title });
                }
            }
            return documentDTOs;
        }
    }
}
