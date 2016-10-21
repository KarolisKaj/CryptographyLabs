namespace StatisticalTesting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public static class CommonData
    {
        public static CommonDTO GetCommonData(IEnumerable<int> data)
        {
            var temp = data.Select(x => Convert.ToString(x, 2).PadLeft(8, '0'));
            string joinedArray = string.Join("", temp.ToArray());
            var zeros = joinedArray.Count(x => x == '0');
            var ones = joinedArray.Count(x => x == '1');
            var total = joinedArray.Count();
            return new CommonDTO(ones, zeros, total, joinedArray);
        }
    }

    public class CommonDTO
    {
        public CommonDTO(int ones, int zeros, int total, string dataSet)
        {
            Zeros = zeros;
            Ones = ones;
            Total = total;
            DataSet = dataSet;
        }

        public int Zeros { get; }
        public int Ones { get; }
        public int Total { get; }
        public string DataSet { get; }
    }
}
