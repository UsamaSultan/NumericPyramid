using System.Collections.Generic;
using System.Threading.Tasks;

namespace NumericPyramid.Dto
{
    public interface INumericPyramidReader
    {
        Task<PagedResult> GetResultAsync(List<int[]> pyramid);
    }
}