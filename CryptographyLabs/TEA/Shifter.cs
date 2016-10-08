namespace TEA
{
    using System;
    internal static class Shifter
    {
        /// <summary>
        /// Only for byte length (8)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        internal static uint RotateLeft(this uint value, int count)
        {
            return (byte)(((value << count) & 0xff) | (value >> (8 - count)));
        }
        /// <summary>
        /// Only for byte length (8)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        internal static uint RotateRight(this uint value, int count)
        {
            return (byte)(((value >> count) & 0xff) | (value << (8 - count)));
        }
        internal static uint[] ShiftArrayRight(this uint[] source)
        {
            uint[] tempKey = new uint[source.Length];
            Array.Copy(source, 0, tempKey, 1, source.Length - 1);
            tempKey[0] = source[source.Length - 1];
            return tempKey;
        }
        internal static uint[] ShiftArrayLeft(this uint[] source)
        {
            uint[] tempKey = new uint[source.Length];
            Array.Copy(source, 1, tempKey, 0, source.Length - 1);
            tempKey[source.Length - 1] = source[0];
            return tempKey;
        }
    }
}
