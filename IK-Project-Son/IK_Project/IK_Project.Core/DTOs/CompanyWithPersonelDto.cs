using IK_Project.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Core.DTOs
{
	public class CompanyWithPersonelDto:CompanyDTO
	{
		public List<PersonelDTO> Personels { get; set; }
		public int PersonelId { get; set; }
		public string Manager { get; set; }

	}
}
