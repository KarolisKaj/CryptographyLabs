namespace StatisticalTesting
{
    using System;
    using System.Collections.Generic;

    internal static class BitPairsTesting
    {
        public static decimal GetT(IEnumerable<int> data)
        {
            var commonData = CommonData.GetCommonData(data);

            var NZeroZero = 0;
            var NZeroOne = 0;
            var NOneZero = 0;
            var NOneOne = 0;
            for (int i = 0; i < commonData.DataSet.Length - 1; i++)
            {
                if ((commonData.DataSet[i].ToString() + commonData.DataSet[i + 1].ToString()) == "00")
                    NZeroZero++;
                if ((commonData.DataSet[i].ToString() + commonData.DataSet[i + 1].ToString()) == "01")
                    NZeroOne++;
                if ((commonData.DataSet[i].ToString() + commonData.DataSet[i + 1].ToString()) == "10")
                    NOneZero++;
                if ((commonData.DataSet[i].ToString() + commonData.DataSet[i + 1].ToString()) == "11")
                    NOneOne++;
            }

            return ((4M / (commonData.Total - 1M)) * ((decimal)Math.Pow(NZeroZero, 2d) + (decimal)Math.Pow(NZeroOne, 2d) + (decimal)Math.Pow(NOneZero, 2d) + (decimal)Math.Pow(NOneOne, 2d)) - (2M / commonData.Total) * ((decimal)Math.Pow(commonData.Zeros, 2d) + (decimal)Math.Pow(commonData.Ones, 2d)) + 1M);
        }
    }
}
