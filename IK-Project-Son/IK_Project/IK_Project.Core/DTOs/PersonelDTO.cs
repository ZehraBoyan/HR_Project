using IK_Project.Core.Entity;
using IK_Project.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Core.DTOs
{
	public class PersonelDTO:BaseDTO
	{
		public string Photo { get; set; }
		public string FirstName { get; set; }
		public Gender Gender { get; set; }
		public string? MiddleName { get; set; }
		public string LastName { get; set; }
		public string? SecondLastName { get; set; }
		public DateTime BirthDate { get; set; }
		public string BirthPlace { get; set; }
		public string TcIdentityNo { get; set; }
		public DateTime HireDate { get; set; }
		public DateTime? DismissalDate { get; set; }

		public string CompanyName { get; set; }
		public string Department { get; set; }
		public string Occupation { get; set; }
		public string Address { get; set; }
		public string? PhoneNumber { get; set; }
		public decimal Salary { get; set; }

		public string Email { get; set; }
		public string Password { get; set; }
		public bool IsActivate { get; set; }
		public PersonStatus PersonStatus { get; set; }
		public int CompanyId { get; set; }
        public List<Leave> Leaves { get; set; }
        public List<ExpenseRequest> Expenses { get; set; }
        public List<CompanyDTO> Companies { get; set; }

        public PersonelDTO()
		{
           
           Leaves = new List<Leave>();
           Expenses = new List<ExpenseRequest>();
			Companies = new List<CompanyDTO>();

          
        }
	}
}
