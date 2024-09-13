using System.Globalization;

namespace STS.SharedKernel.Helpers.Utilties
{
    public partial class CultureUtility
    {
        public static readonly string[] _supportedCultures;
        public static int culture { get; set; }
        static CultureUtility()
        {
            _supportedCultures = new[] {
                  "ar-EG"
                , "en-US"
            };
        }
        public static int CurrentIndex()
        {
            return getIndexOfCulture(Thread.CurrentThread.CurrentCulture.Name);
        }
        private static int getIndexOfCulture(string cultureName)
        {
            if (cultureName.StartsWith("ar-EG"))
                return 0;

            else if (cultureName.StartsWith("en-US"))
                return 1;

            else
                return 0;
        }
        public static CultureInfo[] GetSupportedCulturesInfo()
        {
            var supportedCulturesInfo = new CultureInfo[_supportedCultures.Length];
            for (int i = 0; i < _supportedCultures.Length; i++)
            {
                supportedCulturesInfo[i] = new CultureInfo(_supportedCultures[i].ToString());
            }
            return supportedCulturesInfo;
        }

    }
}
