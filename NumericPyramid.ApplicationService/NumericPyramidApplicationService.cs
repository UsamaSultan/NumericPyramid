using NumericPyramid.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NumericPyramid.ApplicationService
{
    public class NumericPyramidApplicationService
    {
        private readonly INumericPyramidReader _numericPyramidReader;

        public NumericPyramidApplicationService(INumericPyramidReader numericPyramidReader)
        {
            _numericPyramidReader = numericPyramidReader;
        }

        public async Task<PagedResult> GetAsync()
        {
            var streamReader = new StreamReader("input.txt");

            var pyramid = new List<int[]>();
            string text = null;

            while ((text = streamReader.ReadLine()) != null)
            {
                var source = text.Split(' ');
                pyramid.Add(source.Select((string x) => Convert.ToInt32(x.Trim())).ToArray());

            }

            return await _numericPyramidReader.GetResultAsync(pyramid);

        }

    }
}