using CsvHelper;
using CsvHelper.Configuration;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text;

namespace STS.SharedKernel.Helpers.Utilties
{
    public static class ExportUtility
    {
        public static byte[] WriteCsvToMemory<T>(IEnumerable<T> records) where T : class
        {
            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ",",
                Encoding = Encoding.UTF8
            };
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, csvConfig))
            {
                var properties = typeof(T).GetProperties();

                foreach (var property in properties)
                {
                    var displayNameAttribute = (DisplayNameAttribute)property.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault();
                    var headerName = displayNameAttribute != null ? displayNameAttribute.DisplayName : property.Name;
                    csvWriter.WriteField(headerName);
                }
                csvWriter.NextRecord();

                csvWriter.WriteRecords(records);
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }

        public static IWorkbook WriteExcelWithNPOI<T>(IList<T> data, string extension = "xlsx")
        {
            // Get DataTable
            DataTable dt = ConvertListToDataTable(data);
            // Instantiate Wokrbook
            IWorkbook workbook;
            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("The format '" + extension + "' is not supported.");
            }

            ISheet sheet1 = workbook.CreateSheet("Sheet 1");
            //make a header row
            IRow row1 = sheet1.CreateRow(0);

            ICellStyle style = workbook.CreateCellStyle();

            style.BorderBottom = BorderStyle.Medium;
            style.BorderLeft = BorderStyle.Medium;
            style.BorderTop = BorderStyle.Medium;
            style.BorderRight = BorderStyle.Medium;

            style.FillForegroundColor = HSSFColor.Plum.Index2;
            style.FillPattern = FillPattern.SolidForeground;
            for (int j = 0; j < dt.Columns.Count; j++)
            {

                ICell cell = row1.CreateCell(j);
                string columnName = dt.Columns[j].ToString();
                cell.SetCellValue(columnName);
                cell.CellStyle = style;
            }

            //loops through data
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    ICell cell = row.CreateCell(j);
                    string columnName = dt.Columns[j].ToString();
                    cell.SetCellValue(dt.Rows[i][columnName].ToString());
                    cell.CellStyle.WrapText = true; // NOT WORKING
                    sheet1.AutoSizeColumn(j);

                }
            }
            // Auto size columns
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < row1.LastCellNum; j++)
                {
                    sheet1.AutoSizeColumn(j);
                }
            }

            return workbook;
        }

        // Credit: Accepted answer at https://social.msdn.microsoft.com/Forums/vstudio/en-US/6ffcb247-77fb-40b4-bcba-08ba377ab9db/converting-a-list-to-datatable?forum=csharpgeneral
        private static DataTable ConvertListToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.DisplayName, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.DisplayName] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static (byte[], string) GetStreamedFile(IWorkbook workBook)
        {
            string contentType = ""; // Scope
                                     // Credit for two stream since workbook.write() closes the first one: https://stackoverflow.com/a/36584861/6336270 
            MemoryStream tempStream = null;
            MemoryStream stream = null;
            try
            {
                // 1. Write the workbook to a temporary stream
                tempStream = new MemoryStream();
                workBook.Write(tempStream, false);
                // 2. Convert the tempStream to byteArray and copy to another stream
                var byteArray = tempStream.ToArray();
                stream = new MemoryStream();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Seek(0, SeekOrigin.Begin);
                // 3. Set file content type
                contentType = workBook.GetType() == typeof(XSSFWorkbook) ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" : "application/vnd.ms-excel";
                // 4. Return file
                return (stream.ToArray(), contentType);
            }
            finally
            {
                if (tempStream != null) tempStream.Dispose();
                if (stream != null) stream.Dispose();
            }
        }
    }
}
