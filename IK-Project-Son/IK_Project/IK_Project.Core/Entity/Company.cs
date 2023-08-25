using IK_Project.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Core.Entity
{
	public class Company:BaseEntity
	{
		public string Name { get; set; }
		public Title Title { get; set; }
		public string MersisNumber { get; set; }
		public string TaxNumber { get; set; }
		public string TaxAdministration { get; set; }
		public string Logo { get; set; }
		public string? PhoneNumber { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public int? NumberOfEmployees { get; set; }
		public DateTime FoundationYear { get; set; } = DateTime.Now;

        public DateTime ContractStart { get; set; } = DateTime.Now;
        public DateTime? ContractEnd { get; set; } = DateTime.Now;

        public ICollection<Personel> Personels { get; set; }
		public Company()
		{
			Personels = new List<Personel>();
		}

	}
}
