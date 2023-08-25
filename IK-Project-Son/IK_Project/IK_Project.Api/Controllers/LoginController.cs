using AutoMapper;
using IK_Project.Api.Filters;
using IK_Project.Core.DTOs;
using IK_Project.Core.Entity;
using IK_Project.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IK_Project.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : CustomBaseController
	{
		private readonly IMapper _mapper;
		private readonly IPersonelService _personelService;


		public LoginController(IMapper mapper,  IPersonelService personelService)
		{
			_mapper = mapper;
			_personelService = personelService;

		}

		[HttpGet]
		public IActionResult Login(string mail , string password)
		{
			var personel = _personelService.GetDefault(p => p.Email == mail && p.Password == password).FirstOrDefault();

			var personelDto = _mapper.Map<PersonelDTO>(personel);

			return CreateActionResult(CustomResponseDTO<PersonelDTO>.Success(200, personelDto));
		}
	}
}
