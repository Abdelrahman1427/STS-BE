using PathFinder.Common.Helpers.Models;
using QRCoder;
using System.Text;

namespace PathFinder.SharedKernel.Helpers.Utilties
{
    public static class QRCodeUtility
    {
        public static byte[] GeneratedQRCodeWithVCFLink(AddQRCodeForEmployee addQR)
        {
            StringBuilder empData = new StringBuilder();
            empData.Append("BEGIN:VCARD\n");
            empData.Append("VERSION:3.0\n");
            empData.Append($"N:{addQR.LastName};{addQR.FirstName}\n");
            if (addQR.Title != null)
                empData.Append($"FN:{addQR.Title} {addQR.FirstName} {addQR.LastName}\n");
            else
                empData.Append($"FN:{addQR.FirstName} {addQR.LastName}\n");
            empData.Append($"ORG:{addQR.CompanyName}\n");
            if (addQR.Job != null)
                empData.Append($"TITLE:{addQR.Job}\n");
            if (addQR.Mobile != null)
                empData.Append($"TEL;type=CELL;type=VOICE;type=pref:{addQR.Mobile}\n");
            empData.Append($"EMAIL;type=Work;type=pref:{addQR.Email}\n");
            if (addQR.LinkedIn != null)
                empData.Append($"URL;TYPE=Linkedin:{addQR.LinkedIn}\n");
            empData.Append("END:VCARD\n");

            File.WriteAllText(addQR.VCFURL, empData.ToString());
            // Generate the QR code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(empData.ToString(), QRCodeGenerator.ECCLevel.Q);

            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);

            var qrCodeImage = qrCode.GetGraphic(3);
            return qrCodeImage;
        }
    }

}
