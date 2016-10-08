namespace VigenereCipher
{
    using System;
    using System.IO;

    internal static class FileHelper
    {
        internal static string GetMessage(string fullFileName)
        {
            var text = File.ReadAllText($@"..\..\{fullFileName}");
            var key = text.Substring(4, text.IndexOf(Environment.NewLine) - 4);
            return text.Contains("Key") ? text.Remove(0, key.Length + 4).Replace(Environment.NewLine, "").Replace(" ", "") : text.Replace(Environment.NewLine, "").Replace(" ", "");
        }

        internal static string GetKey(string fullFileName)
        {
            var text = File.ReadAllText($@"..\..\{fullFileName}");
            return text.Substring(4, text.IndexOf(Environment.NewLine) - 4);
        }
    }
}
