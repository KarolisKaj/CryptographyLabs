namespace TEA
{
    using System;
    internal static class TEA
    {
        internal static void Decrypt(uint[,] cipher, uint[] key)
        {
            for (uint i = 0; i < cipher.GetLength(0); i++)
            {
                key = key.ShiftArrayRight();
                var result = Iteration(new Tuple<uint, uint>(cipher[i, 0], cipher[i, 1]), key, FinalActionDecryption);
                key = key.ShiftArrayRight();
                result = Iteration(new Tuple<uint, uint>(result.Item1, result.Item2), key, FinalActionDecryption);
                key = key.ShiftArrayRight();
                result = Iteration(new Tuple<uint, uint>(result.Item1, result.Item2), key, FinalActionDecryption);
                Console.Write($"{(char)result.Item2}{(char)result.Item1}");
            }
            Console.WriteLine();
        }
        internal static void Encrypt(uint[,] plainText, uint[] key)
        {
            for (uint i = 0; i < plainText.GetLength(0); i++)
            {
                var result = Iteration(new Tuple<uint, uint>(plainText[i, 0], plainText[i, 1]), key, FinalActionEncryption);
                key = key.ShiftArrayLeft();
                result = Iteration(new Tuple<uint, uint>(result.Item1, result.Item2), key, FinalActionEncryption);
                key = key.ShiftArrayLeft();
                result = Iteration(new Tuple<uint, uint>(result.Item1, result.Item2), key, FinalActionEncryption);
                key = key.ShiftArrayLeft();
                Console.WriteLine($"{result.Item2},{result.Item1}");
            }
        }
        internal static void DecryptCBC(uint[,] cipher, uint[] key, Tuple<uint, uint> IV)
        {
            for (uint i = 0; i < cipher.GetLength(0); i++)
            {
                key = key.ShiftArrayRight();
                var result = Iteration(new Tuple<uint, uint>(cipher[i, 0], cipher[i, 1]), key, FinalActionDecryption);
                key = key.ShiftArrayRight();
                result = Iteration(new Tuple<uint, uint>(result.Item1, result.Item2), key, FinalActionDecryption);
                key = key.ShiftArrayRight();
                result = Iteration(new Tuple<uint, uint>(result.Item1, result.Item2), key, FinalActionDecryption);
                Console.Write($"{(char)(result.Item2 ^ IV.Item1)}{(char)(result.Item1 ^ IV.Item2)}");
                IV = new Tuple<uint, uint>(cipher[i, 0], cipher[i, 1]);
            }
            Console.WriteLine();
        }
        internal static void DecryptOFB(uint[,] cipher, uint[] key, Tuple<uint, uint> IV)
        {
            for (uint i = 0; i < cipher.GetLength(0); i++)
            {
                var result = Iteration(new Tuple<uint, uint>(IV.Item1, IV.Item2), key, FinalActionEncryption);
                key = key.ShiftArrayLeft();
                result = Iteration(new Tuple<uint, uint>(result.Item1, result.Item2), key, FinalActionEncryption);
                key = key.ShiftArrayLeft();
                result = Iteration(new Tuple<uint, uint>(result.Item1, result.Item2), key, FinalActionEncryption);
                key = key.ShiftArrayLeft();
                IV = new Tuple<uint, uint>(result.Item2, result.Item1);
                Console.Write($"{(char)(result.Item2 ^ cipher[i, 0])}{(char)(result.Item1 ^ cipher[i, 1])}");
            }
            Console.WriteLine();
        }
        private static Tuple<uint, uint> Iteration(Tuple<uint, uint> pair, uint[] key, Func<uint, uint, uint> lastAction)
        {
            var firstSumResult = (pair.Item1 + key[0]);
            var shiftLeft = pair.Item1.RotateLeft(2);
            var upperLane = (shiftLeft + key[1]);
            var shiftRight = pair.Item1.RotateRight(2);
            var lowerLane = (shiftRight + key[2]);
            uint result = lastAction((firstSumResult ^ lowerLane ^ upperLane), pair.Item2) % 256;
            return new Tuple<uint, uint>(result, pair.Item1);
        }
        private static uint FinalActionDecryption(uint xorOutput, uint otherCipherItem)
        {
            return otherCipherItem - xorOutput;
        }
        private static uint FinalActionEncryption(uint xorOutput, uint otherCipherItem)
        {
            return otherCipherItem + xorOutput;
        }
    }
}
