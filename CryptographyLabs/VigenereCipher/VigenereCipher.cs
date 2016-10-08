namespace VigenereCipher
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    internal static class VigenereCipher
    {
        public static string Alphabet { get; set; } = "AĄBCČDEĘĖFGHIĮYJKLMNOPRSŠTUŲŪVZŽ";
        public static Dictionary<char, decimal> LithuanianAlphabetFrequency { get; set; } = new Dictionary<char, decimal>()
            {
                { 'A', 11.1M},
                { 'Ą', 0.8M },
                { 'B', 1.6M },
                { 'C', 0.5M },
                { 'Č', 0.3M },
                { 'D', 2.5M },
                { 'E', 6.4M },
                { 'Ę', 0.2M },
                { 'Ė', 0.6M },
                { 'F', 0.2M },
                { 'G', 1.8M },
                { 'H', 0.1M },
                { 'I', 14M },
                { 'Į', 0.6M },
                { 'Y', 1.4M },
                { 'J', 2.1M },
                { 'K', 4.7M },
                { 'L', 2.9M },
                { 'M', 3.8M },
                { 'N', 5.2M },
                { 'O', 6M },
                { 'P', 2.9M },
                { 'R', 5.4M },
                { 'S', 7.7M },
                { 'Š', 1.3M },
                { 'T', 5.8M },
                { 'U', 5.1M },
                { 'Ų', 1.5M },
                { 'Ū', 0.2M },
                { 'V', 2.2M },
                { 'Z', 0.2M },
                { 'Ž', 0.9M },
            };

        public static IEnumerable<string> DecryptFromFrequencies(this string message, int keyLength)
        {
            var lettersOfFrequency = new Dictionary<char, decimal>[keyLength];
            for (int i = 0; i < keyLength; i++)
            {
                var tempMatchCache = new decimal[Alphabet.Length];
                for (int j = 0; j < message.Length; j++)
                {
                    if (lettersOfFrequency[j % keyLength] == null)
                        lettersOfFrequency[j % keyLength] = GetIntitializedDictionary();

                    if (lettersOfFrequency[j % keyLength].ContainsKey(message[j]))
                        lettersOfFrequency[j % keyLength][message[j]]++;
                    else
                        lettersOfFrequency[j % keyLength].Add(message[j], 1);
                }
            }
            var rotation = GetKeysRotations(lettersOfFrequency);

            yield return String.Format("Key found = {0}", String.Join("", rotation.Select(x => Alphabet[x])));
            yield return Decrypt(message, rotation);
        }

        public static string Decrypt(this string message, int[] keyLength)
        {
            Contract.Assert(message != null);

            string output = String.Empty;
            for (int i = 0; i < message.Length; i++)
            {
                output += Alphabet[(Alphabet.IndexOf(message[i]) + Alphabet.Length - keyLength[i % keyLength.Length]) % Alphabet.Length];
            }
            return output;
        }


        public static string Decrypt(this string message, string key)
        {
            Contract.Assert(message != null && key != null);
            //var keyInAlphabetIndex = key.Select(x => Alphabet.IndexOf(Alphabet[(Alphabet.Length - Alphabet.IndexOf(x)) % Alphabet.Length])).ToArray();
            var keyInAlphabetIndex = key.Select(x => Alphabet.IndexOf(x)).ToArray();

            string output = String.Empty;
            for (int i = 0; i < message.Length; i++)
            {
                output += Alphabet[(Alphabet.IndexOf(message[i]) + Alphabet.Length - keyInAlphabetIndex[i % keyInAlphabetIndex.Length]) % Alphabet.Length];
            }
            return output;
        }
        public static void ShowIC(this string message)
        {
            var dictionaryOccurenceRates = new List<Decimal>();
            decimal[] result = new decimal[50];
            for (int i = 2; i < 50; i++)
            {
                var splitten = GetSplitsBy(message, i);
                dictionaryOccurenceRates.Clear();
                var dictionariesOfOccurencies = GetAllCountsForOccurences(splitten);
                for (int j = 0; j < dictionariesOfOccurencies.Length; j++)
                {
                    dictionaryOccurenceRates.Add(Alphabet.Sum(x => (decimal)((decimal)dictionariesOfOccurencies[j][x.ToString()] * (decimal)(dictionariesOfOccurencies[j][x.ToString()] - 1M)) / (decimal)((decimal)dictionariesOfOccurencies[j].Count * ((decimal)dictionariesOfOccurencies[j].Count - 1M))));
                }
                result[i] = dictionaryOccurenceRates.Average();
                Console.WriteLine($"{i}. {result[i]}");
            }
        }
        public static void PutOutOccurences(string message)
        {
            int ocurrenceCount = 0;
            for (int i = 1; i < message.Length; i++)
            {
                ocurrenceCount = 0;
                for (int j = 0; j < message.Length; j++)
                {
                    if (i + j < message.Length)
                        ocurrenceCount = message[i + j] == message[j] ? ocurrenceCount + 1 : ocurrenceCount;
                    else
                        continue;
                }
                Console.WriteLine($"{i}, {ocurrenceCount}");
            }
        }
        private static Dictionary<string, int>[] GetAllCountsForOccurences(List<char>[] splits)
        {
            var result = new Dictionary<string, int>[splits.Length];
            for (int i = 0; i < splits.Length; i++)
            {
                foreach (var letter in Alphabet)
                {
                    if (result[i] == null)
                        result[i] = new Dictionary<string, int>() { { letter.ToString(), splits[i].Count(x => x == letter) } };
                    else
                        result[i].Add(letter.ToString(), splits[i].Count(x => x == letter));
                }
            }
            return result;
        }
        private static List<char>[] GetSplitsBy(string message, int splitBy)
        {
            var result = new List<char>[splitBy];
            for (int i = 0; i < message.Length; i++)
            {
                if (result[i % splitBy] == null)
                    result[i % splitBy] = new List<char>() { message[i] };
                else
                    result[i % splitBy].Add(message[i]);
            }
            return result;
        }
        private static int[] GetKeysRotations(Dictionary<char, decimal>[] keySequences)
        {
            var results = new int[keySequences.Length];
            var highestProbability = 0M;
            var currentRotationProbability = 0M;

            for (int t = 0; t < keySequences.Length; t++)
            {
                highestProbability = 0;
                for (int i = 0; i < LithuanianAlphabetFrequency.Count; i++)
                {
                    currentRotationProbability = 0;
                    for (int j = 0; j < keySequences[t].Count; j++)
                    {
                        currentRotationProbability += (decimal)((decimal)LithuanianAlphabetFrequency.Values.ElementAt(j) * (decimal)keySequences[t].Values.ElementAt((j + i) % Alphabet.Length));
                    }
                    if (currentRotationProbability > highestProbability)
                    {
                        highestProbability = currentRotationProbability;
                        results[t] = i;
                    }
                }
            }
            return results;
        }
        private static Dictionary<char, decimal> GetIntitializedDictionary()
        {
            var dictionary = new Dictionary<char, decimal>();
            foreach (var letter in Alphabet)
            {
                dictionary.Add(letter, 0);
            }
            return dictionary;
        }
    }
}
