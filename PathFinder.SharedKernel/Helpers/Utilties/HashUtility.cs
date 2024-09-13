using PasswordGenerator;

namespace STS.SharedKernel.Helpers.Utilties
{
    public static class HashUtility
    {
        public static string GetStringSha256Hash(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }
        public static string GetComplexPassword() => new Password(10).IncludeLowercase()
                .IncludeUppercase().IncludeSpecial().IncludeNumeric().Next();

    }
}


