namespace STS.SharedKernel.Helpers.Utilties
{
    public static class ValidationUtility
    {
        public static bool ValidateLink(string Link)
        {
            bool isLink = Link is string valueAsString &&
                (valueAsString.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                || valueAsString.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
                || valueAsString.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase));
            return isLink;
        }
    }
}
