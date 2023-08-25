using AutoMapper;
using IK_Project.Api.Filters;
using IK_Project.Core.DTOs;
using IK_Project.Core.Entity;
using IK_Project.Core.Services;
using IK_Project.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace IK_Project.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController : CustomBaseController
	{
		private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
		{
			_companyService = companyService;
            _mapper = mapper;
		}

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var companies = await _companyService.GetAllAsync();
            var companyDtos = _mapper.Map<List<CompanyDTO>>(companies.ToList());

            return CreateActionResult(CustomResponseDTO<List<CompanyDTO>>.Success(200, companyDtos));
        }
        [HttpGet("{companyId}")]
		public async Task<IActionResult> GetSingleCompanyByIdWithPersonel(int companyId)
		{

			return CreateActionResult(await _companyService.GetSingleCompanyByIdWithPersonelAsync(companyId));
		}
        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody]CompanyWithPersonelDto companyDtos)
        {
            var mapped = _mapper.Map<Company>(companyDtos);

            var personel = await _companyService.AddAsync(mapped);


            return Ok(personel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(CompanyDTO companyDtos)
        {

            var mapped = _mapper.Map<Company>(companyDtos);
            var company = await _companyService.AddAsync(mapped);
            var companyDto = _mapper.Map<CompanyDTO>(company);


            //return CreateActionResult(CustomResponseDTO<CompanyDTO>.Success(201,companyDto));
            return Ok(companyDto);
        }

    }
}
