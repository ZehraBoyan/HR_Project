using IK_Project.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Core.Entity
{
	public class ExpenseRequest:BaseEntity
	{
		public ExpenseType ExpenseType { get; set; }
		public decimal SpentAmount { get; set; }
		public string Currency { get; set; }
		public string ApprovalStatus { get; set; }
		public DateTime RequestDate { get; set; }
		public DateTime? ResponseDate { get; set; }
		public string DocumentPath { get; set; }
		public ExpenseStatus Status { get; set; } = ExpenseStatus.Pending;

		//-----------------------------//

		public ICollection<Personel> Personels { get; set; }
		public ExpenseRequest()
		{
			Personels = new List<Personel>();
		}

	}
}
