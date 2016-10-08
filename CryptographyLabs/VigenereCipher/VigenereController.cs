namespace VigenereCipher
{
    using System;
    /// <summary>
    /// To get key length you must call method VigenereCipher.PutOutOccurences(currentMessage) and manually find length from output.
    /// </summary>
    public static class VigenereController
    {
        public static void ShowAnswers()
        {
            Console.WriteLine("Showing Vigenere decryption:");

            var currentKey = FileHelper.GetKey("VigenereFirstTask.txt");
            var currentMessage = FileHelper.GetMessage("VigenereFirstTask.txt");
            Console.WriteLine($"Vigenere first decryption. Key = {currentKey}:");
            foreach (var decryption in VigenereCipher.Decrypt(currentMessage, currentKey))
            {
                Console.Write(decryption);
            }
            
            currentMessage = FileHelper.GetMessage("VigenereSecondTask.txt");
            Console.WriteLine($"{Environment.NewLine}Vigenere second decryption.");
            foreach (var decryption in VigenereCipher.DecryptFromFrequencies(currentMessage, 7))
            {
                Console.WriteLine(decryption);
            }

            currentMessage = FileHelper.GetMessage("VigenereThirdTask.txt");
            Console.WriteLine($"Vigenere third decryption.");
            foreach (var decryption in VigenereCipher.DecryptFromFrequencies(currentMessage, 6))
            {
                Console.WriteLine(decryption);
            }

            currentKey = FileHelper.GetKey("VigenereFouthTask.txt");
            currentMessage = FileHelper.GetMessage("VigenereFouthTask.txt");
            Console.WriteLine($"Vigenere fourth decryption. Key = {currentKey}:");
            Console.WriteLine("To be implemented.");
            Console.ReadLine();
        }
    }
}
