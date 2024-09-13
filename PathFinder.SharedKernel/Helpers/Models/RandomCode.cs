
using STS.SharedKernel.Constants;

namespace STS.Common.Helpers.Models
{
    public static class RandomQrCode
    {
        public static string random()
        {
            Random res = new Random();
            String ran = string.Empty;

            for (int i = 0; i < 16; i++)
            {
                int x = res.Next(62);
                ran = ran + AppConstants.RandomString[x];
            }
            return ran;

        }
    }
}
