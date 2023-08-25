using AutoMapper;
using IK_Project.Api.Filters;
using IK_Project.Core.DTOs;
using IK_Project.Core.Entity;
using IK_Project.Core.Enums;
using IK_Project.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace IK_Project.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonelController : CustomBaseController
	{
		private readonly IMapper _mapper;
		private readonly IPersonelService _personelService;


		public PersonelController(IMapper mapper,  IPersonelService personelService)
		{
			_mapper = mapper;
			_personelService = personelService;

		}

		[HttpGet]
		public async Task<IActionResult> GetPersonelWithCompany([FromQuery]List<PersonelWithCompanyDto> perCom)
		{
			var result = (await _personelService.GetPersonelWithCompany());
			return (IActionResult)result;
		}
        [HttpGet]
		public async Task<IActionResult> GetManagers()
		{
            var managers = await _personelService.Where(p => p.PersonStatus == PersonStatus.manager).ToListAsync();
			var managerDTO = _mapper.Map<List<PersonelDTO>>(managers);
            return CreateActionResult(CustomResponseDTO<List<PersonelDTO>>.Success(200, managerDTO));
        }

        [HttpGet]
		public async Task<IActionResult> All()
		{
			var personels=await _personelService.GetAllAsync();
			var personelDtos=_mapper.Map<List<PersonelDTO>>(personels.ToList());
			//return Ok (CustomResponseDTO<List<PersonelDTO>>.Success(200, personelDtos));

			return CreateActionResult(CustomResponseDTO<List<PersonelDTO>>.Success(200, personelDtos));
		}

		[ServiceFilter(typeof(NotFoundFilter<Personel>))]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var personelId = await _personelService.GetByIdAsync(id);
		
			var personelDtos = _mapper.Map<PersonelDTO>(personelId);

			return CreateActionResult(CustomResponseDTO<PersonelDTO>.Success(200, personelDtos));
		}

		[HttpPost]
		public async Task<IActionResult> Save(PersonelDTO personelDto)
		{
			var personel = await _personelService.AddAsync(_mapper.Map<Personel>(personelDto));
			var personelDtos = _mapper.Map<PersonelDTO>(personel);

			return CreateActionResult(CustomResponseDTO<PersonelDTO>.Success(201, personelDtos));
		}

		[HttpPut]
		public async Task<IActionResult> Update(PersonelDTO personelDto)
		{
			await _personelService.UpdateAsync(_mapper.Map<Personel>(personelDto));

			return CreateActionResult(CustomResponseDTO<PersonelDTO>.Success(204));
		}

		[ServiceFilter(typeof(NotFoundFilter<Personel>))]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Remove(int id)
		{
			var personel = await _personelService.GetByIdAsync(id);
			await _personelService.RemoveAsync(personel);

			return CreateActionResult(CustomResponseDTO<PersonelDTO>.Success(204));
		}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonelIncludeLeaves(int id)
        {
            var personelId = await _personelService.GetByIdAsync(id);
            var personelDTO = _mapper.Map<PersonelDTO>(personelId);
            var leaves = personelDTO.Leaves.ToList();
            return Ok(leaves);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonelIncludeExpense(int id)
        {
			var personelId = await _personelService.GetByIdAsync(id);
			var personelDTO=_mapper.Map<PersonelDTO>(personelId);
			var expense=personelDTO.Expenses.ToList();
			return Ok(expense);
        }

    }
}
