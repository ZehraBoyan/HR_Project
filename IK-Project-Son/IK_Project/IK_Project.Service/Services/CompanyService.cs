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
	public class CompanyService : Service<Company>, ICompanyService
	{
		private readonly ICompanyRepository _companyRepository;
		private readonly IMapper _mapper;

		public CompanyService(IGenericRepository<Company> repository, IUnitOfWorks unitOfWork, IMapper mapper,ICompanyRepository companyRepository) : base(repository, unitOfWork)
		{
			_mapper= mapper;
			_companyRepository= companyRepository;
		}

		public async Task<CustomResponseDTO<CompanyWithPersonelDto>> GetSingleCompanyByIdWithPersonelAsync(int companyId)
		{
			var company = await _companyRepository.GetSingleCompanyByIdWithPersonelAsync(companyId);
			var companyDto=_mapper.Map<CompanyWithPersonelDto>(company);
			return CustomResponseDTO<CompanyWithPersonelDto>.Success(200,companyDto);
		}

		
	}
}
