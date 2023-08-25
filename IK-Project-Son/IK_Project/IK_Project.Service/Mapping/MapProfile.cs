using AutoMapper;
using IK_Project.Core.DTOs;
using IK_Project.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Service.Mapping
{
	public class MapProfile:Profile
	{
		public MapProfile()
		{
			CreateMap<Company, CompanyDTO>().ReverseMap();
			CreateMap<Personel, PersonelDTO>().ReverseMap();
			CreateMap<Personel, PersonelWithCompanyDto>();
			CreateMap<Company, CompanyWithPersonelDto>();


		}
	}
}
