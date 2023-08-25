using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Core.DTOs
{
	public class PersonelWithCompanyDto:PersonelDTO
	{
		public CompanyDTO Company { get; set; }
	}
}
