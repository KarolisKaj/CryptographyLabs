namespace TEA
{
    using System;
    public static class TEAController
    {
        public static void ShowAnswers()
        {
            uint[] key = new uint[3] { 105, 101, 116 };
            uint[,] cipher = new uint[,] { { 242, 40 }, { 164, 218 }, { 78, 28 }, { 223, 231 }, { 255, 93 }, { 253, 116 }, { 78, 222 }, { 148, 111 } };
            Console.WriteLine("TEA decryption:");
            TEA.Decrypt(cipher, key);

            uint[,] cipher2 = new uint[,] { { 115, 22 }, { 224, 181 }, { 208, 211 }, { 182, 246 }, { 130, 70 }, { 36, 109 }, { 101, 163 }, { 31, 135 }, { 234, 72 }, { 138, 78 }, { 93, 79 }, { 174, 192 } };
            Tuple<uint, uint> IV2 = new Tuple<uint, uint>('t', 'u');
            Console.WriteLine("TEA CBC decryption:");
            TEA.DecryptCBC(cipher2, key, IV2);

            uint[,] cipher3 = new uint[,] { { 28, 57 }, { 248, 222 }, { 92, 143 }, { 216, 205 }, { 150, 89 }, { 17, 92 }, { 119, 176 }, { 210, 27 }, { 54, 248 }, { 143, 84 }, { 180, 238 }, { 9, 21 } };
            Tuple<uint, uint> IV3 = new Tuple<uint, uint>('p', 'l');
            Console.WriteLine("TEA OFB decryption:");
            TEA.DecryptOFB(cipher3, key, IV3);

            // Check whether encryption model is working correctly and you actually can encrypt and decrypt same message/cipher.
            //TEA.Encrypt(new uint[,] { { 'l', 'i' }, { 'e', 't' }, { 'i', 'n' }, { 'g', 'a' }, { 's', ' ' }, { ' ', 'o' }, { 'r', 'a' }, { 's', 'x' } }, key);
        }
    }
}
