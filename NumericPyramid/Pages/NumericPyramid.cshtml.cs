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
        private readonly ILogger<NumericPyramidModel> _logger;
        private readonly NumericPyramidApplicationService _numericPyramidApplicationService;
        private readonly IMapper _mapper;

        public NumericPyramidModel(ILogger<NumericPyramidModel> logger,
            NumericPyramidApplicationService numericPyramidApplicationService,
            IMapper mapper)
        {
            _logger = logger;
            _numericPyramidApplicationService = numericPyramidApplicationService;
            _mapper = mapper;
        }

        public PagedResult Result { get; private set; }

        public async void OnGet()
        {
            try
            {
                var result = await _numericPyramidApplicationService.GetAsync();
                Result = _mapper.Map<PagedResult>(result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }

        }
    }
}