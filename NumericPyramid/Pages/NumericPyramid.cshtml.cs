using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NumericPyramid.ApplicationService;
using NumericPyramid.Dto;
using System;

namespace NumericPyramid.Pages
{
    public class NumericPyramidModel : PageModel
    {
        private IMapper Mapper { get; }
        private readonly NumericPyramidApplicationService _numericPyramidApplicationService;

        public NumericPyramidModel(NumericPyramidApplicationService numericPyramidApplicationService, IMapper mapper)
        {
            Mapper = mapper;
            _numericPyramidApplicationService = numericPyramidApplicationService;
        }

        public PagedResult Result { get; private set; }

        public async void OnGet()
        {
            try
            {
                var result = await _numericPyramidApplicationService.GetAsync();
                Result = Mapper.Map<PagedResult>(result);
            }
            catch (Exception exception)
            {
            }

        }
    }
}