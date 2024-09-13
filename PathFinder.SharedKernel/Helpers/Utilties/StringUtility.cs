using System.Text.RegularExpressions;

namespace STS.SharedKernel.Helpers.Utilties
{
    public static class StringUtility
    {
        public static string StripHTMLAndCheckVisible(string HTMLText)
        {
            if (string.IsNullOrEmpty(HTMLText))
                return "";
            else
            {
                Regex regJs = new Regex(@"(?s)<\s?script.*?(/\s?>|<\s?/\s?script\s?>)", RegexOptions.IgnoreCase);
                HTMLText = regJs.Replace(HTMLText, "");
                Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
                HTMLText = reg.Replace(HTMLText, "");
                HTMLText = HTMLText.Replace("&nbsp;", "");
                return HTMLText;
            }
        }

        public static List<string> SplitStringIntoChunks(string input, int chunkSize)
        {
            List<string> chunks = new List<string>();

            for (int i = 0; i < input.Length; i += chunkSize)
            {
                int remainingLength = Math.Min(chunkSize, input.Length - i);
                string chunk = input.Substring(i, remainingLength);
                chunks.Add(chunk);
            }

            return chunks;
        }
    }
}
