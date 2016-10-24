namespace StatisticalTesting
{
    using CryptographyLabs.Utilities;
    using System;

    public static class StatisticalTestingController
    {
        public static void ShowAnswers()
        {
            Console.WriteLine($"{Environment.NewLine}Statistical testing:");
            Console.WriteLine($"T1 = {BitTesting.GetT(FileHelper.GetArray("FirstChiper.txt"))}, for first cipher.");
            Console.WriteLine($"T2 = {BitPairsTesting.GetT(FileHelper.GetArray("FirstChiper.txt"))}, for first cipher.");

            Console.WriteLine($"T1 = {BitTesting.GetT(FileHelper.GetArray("SecondChiper.txt"))}, for second cipher.");
            Console.WriteLine($"T2 = {BitPairsTesting.GetT(FileHelper.GetArray("SecondChiper.txt"))}, for second cipher.");
        }
    }
}
