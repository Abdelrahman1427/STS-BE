using PathFinder.Core.Interface.Shared.IServices;
using PathFinder.DataTransferObjects.Helpers;
using PathFinder.SharedKernel.Constants;
using RoboGas.DataTransferObjects.Helpers;
using System.IO;
using System.Xml;

namespace PathFinder.BusinessLogic.Services.Shared
{
    public class FileErrorService : ILoggerTypeService
    {
        private readonly FileLogs _fileLogs;
        public FileErrorService(FileLogs fileLogs)
        {
            _fileLogs = fileLogs;
        }

        public async Task<APIResult> SaveAction(Logger logger)
        {
            try
            {
                string date = logger.CreatedDate?.ToString().Split(" ")[0] + AppConstants.XML;
                date = date.Replace(@"/", string.Empty);
                date = _fileLogs.FileAction + date;

                string folderName = AppContext.BaseDirectory + AppConstants.LoggerException;
                string fileName = Path.Combine(folderName, date);

                if (!Directory.Exists(folderName))
                    Directory.CreateDirectory(folderName);

                if (!File.Exists(fileName))
                {
                    XmlWriter writer = XmlWriter.Create(fileName);
                    writer.WriteStartDocument();
                    writer.WriteStartElement(AppConstants.Path);
                    writer.WriteStartElement(AppConstants.Logger);

                    writer.WriteStartElement(AppConstants.Path);
                    writer.WriteString(logger.Path);
                    writer.WriteEndElement();

                    writer.WriteStartElement(AppConstants.Message);
                    writer.WriteString(logger.Message);
                    writer.WriteEndElement();

                    writer.WriteStartElement(AppConstants.CreatedDate);
                    writer.WriteString(logger.CreatedDate.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement(AppConstants.CreatedBy);
                    writer.WriteString(logger.CreatedBy);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Close();
                }

                else
                {
                    XmlDocument doc = new XmlDocument();

                    XmlElement Logger = doc.CreateElement(AppConstants.Logger);

                    XmlElement node = doc.CreateElement(AppConstants.Path);
                    node.InnerText = logger.Path;
                    Logger.AppendChild(node);

                    node = doc.CreateElement(AppConstants.Message);
                    node.InnerText = logger.Message;
                    Logger.AppendChild(node);

                    node = doc.CreateElement(AppConstants.CreatedDate);
                    node.InnerText = logger.CreatedDate.ToString();
                    Logger.AppendChild(node);

                    node = doc.CreateElement(AppConstants.CreatedBy);
                    node.InnerText = logger.CreatedBy;
                    Logger.AppendChild(node);

                    doc.Load(fileName);
                    XmlElement root = doc.DocumentElement;
                    root.AppendChild(Logger);
                    doc.Save(fileName);
                }
                return new APIResult { state = true };
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return new APIResult { message = ex.InnerException.Message };

                return new APIResult { message = ex.Message };
            }

        }
    }
}
