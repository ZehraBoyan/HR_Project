using AutoMapper;
using IK_Project.Core.DTOs;
using IK_Project.Core.Entity;
using IK_Project.Core.Repositories;
using IK_Project.Core.Services;
using IK_Project.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Service.Services
{
	public class PersonelService : Service<Personel>, IPersonelService
	{
		private readonly IPersonelRepository _personelRepository;
		private readonly IMapper _mapper;

		public PersonelService(IGenericRepository<Personel> repository, IUnitOfWorks unitOfWork, IPersonelRepository personelRepository, IMapper mapper) : base(repository, unitOfWork)
		{
			_personelRepository = personelRepository;
			_mapper = mapper;
		}

		public async Task<CustomResponseDTO<List<PersonelWithCompanyDto>>> GetPersonelWithCompany()
		{
			var personels = await _personelRepository.GetPersonelWithCompany();
			var personelDTO = _mapper.Map<List<PersonelWithCompanyDto>>(personels);
			return CustomResponseDTO<List<PersonelWithCompanyDto>>.Success(200,personelDTO);
		}
	}
}
