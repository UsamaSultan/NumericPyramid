using Microsoft.Extensions.Logging;
using NumericPyramid.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumericPyramid.DataAccess
{
    public class NumericPyramidReader : INumericPyramidReader
    {
        private readonly ILogger<NumericPyramidReader> _logger;

        public NumericPyramidReader(ILogger<NumericPyramidReader> logger)
        {
            _logger = logger;
        }
        public async Task<PagedResult> GetResultAsync(List<int[]> pyramid)
        {
            _logger.LogInformation("Calculating Result from Reader.");

            var even = pyramid[0][0] % 2;
            var top = pyramid[0][0];
            var count = pyramid.Count();
            var prevStep = new int[count + 1][];
            var maxValue = new int[count + 1][];

            for (var i = 0; i < count; i++)
            {
                prevStep[i] = new int[count + 1];
                maxValue[i] = new int[count + 1];
            }

            prevStep[0][0] = -1;
            maxValue[0][0] = top;

            for (var j = 1; j < count; j++)
            {
                for (var k = 0; k < pyramid[j].Length; k++)
                {
                    var value = pyramid[j][k];
                    if (value % 2 == even)
                    {
                        prevStep[j][k] = -1;
                        maxValue[j][k] = -1;
                        continue;
                    }
                    if (k > 0 && maxValue[j - 1][k - 1] != -1)
                    {
                        maxValue[j][k] = maxValue[j - 1][k - 1] + value;
                        prevStep[j][k] = k - 1;
                    }
                    if (maxValue[j - 1][k] != -1 && maxValue[j - 1][k] + value > maxValue[j][k])
                    {
                        maxValue[j][k] = maxValue[j - 1][k] + value;
                        prevStep[j][k] = k;
                    }
                }
                even ^= 1;
            }

            var list = new List<int>();

            top = maxValue[count - 1].Max();
            var maxSum = Convert.ToString(top);

            var tempValue = maxValue[count - 1].ToList().IndexOf(top);
            var prevCount = count - 1;

            while (prevCount >= 0)
            {
                list.Add(pyramid[prevCount][tempValue]);
                tempValue = prevStep[prevCount][tempValue];
                prevCount--;
            }

            list.Reverse();
            var pathValue = list.Select((int number) => $"{number}").ToArray();
            var path = string.Join("->", pathValue);

            return new PagedResult()
            {
                Path = path,
                MaxSum = maxSum
            };
        }
    }
}
