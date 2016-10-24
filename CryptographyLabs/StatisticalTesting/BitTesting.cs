namespace StatisticalTesting
{
    using System.Collections.Generic;

    internal static class BitTesting
    {
        internal static decimal GetT(IEnumerable<int> data)
        {
            var commonData = CommonData.GetCommonData(data);

            var dif = commonData.Ones - commonData.Zeros;
            return dif * (decimal)dif / commonData.Total;
        }
    }
}
