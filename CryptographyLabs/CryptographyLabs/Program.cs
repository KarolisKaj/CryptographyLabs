namespace CryptographyLabs
{

    using System;
    class Program
    {
        static void Main(string[] args)
        {
            VigenereCipher.VigenereController.ShowAnswers();
            Console.ReadKey();
            TEA.TEAController.ShowAnswers();
            Console.ReadKey();
            StatisticalTesting.StatisticalTestingController.ShowAnswers();
            Console.ReadKey();
        }
    }
}
