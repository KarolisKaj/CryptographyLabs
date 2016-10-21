namespace CryptographyLabs.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    public static class FileHelper
    {
        public static string GetMessage(string fullFileName)
        {
            var text = File.ReadAllText($@"..\..\{fullFileName}");
            var key = text.Substring(4, text.IndexOf(Environment.NewLine) - 4);
            return text.Contains("Key") ? text.Remove(0, key.Length + 4).Replace(Environment.NewLine, "").Replace(" ", "") : text.Replace(Environment.NewLine, "").Replace(" ", "");
        }

        public static string GetKey(string fullFileName)
        {
            var text = File.ReadAllText($@"..\..\{fullFileName}");
            return text.Substring(4, text.IndexOf(Environment.NewLine) - 4);
        }

        public static IEnumerable<int> GetArray(string fullFileName)
        {


            var filePath = FindFileInDir(fullFileName, @"..\..\..\");
            var text = File.ReadAllText(filePath);


            foreach (var match in Regex.Matches(text, @"\d+"))
            {
                yield return Int32.Parse(match.ToString());
            }
        }

        public static string FindFileInDir(string fileName, string startLocation)
        {
            var results = Directory.GetFiles(startLocation, fileName, SearchOption.AllDirectories);
            return results.Length > 0 ? results[0] : String.Empty;
        }
    }
}
