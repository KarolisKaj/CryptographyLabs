namespace VigenereCipher
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    internal static class VigenereAutoKeyCipher
    {
        //public static string Alphabet { get; set; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string Alphabet { get; set; } = "AĄBCČDEĘĖFGHIĮYJKLMNOPRSŠTUŲŪVZŽ";
        public static IEnumerable<string> Decrypt(string message, string key)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < message.Length; i++)
            {
                key += Alphabet[(Alphabet.Length + Alphabet.Length - Alphabet.IndexOf(key[i]) - Alphabet.IndexOf(message[i])) % Alphabet.Length];
                sb.Append(key[key.Length - 1]);
            }
            yield return String.Format("Key created = {0}{1}", key, Environment.NewLine);
            yield return sb.ToString();
        }
    }
}
