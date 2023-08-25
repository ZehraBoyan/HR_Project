using IK_Project.Core.DTOs;
using IK_Project.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Core.Services
{
	public interface IPersonelService:IService<Personel>
	{
		Task<CustomResponseDTO<List<PersonelWithCompanyDto>>> GetPersonelWithCompany();
	}
}
