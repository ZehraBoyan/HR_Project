using IK_Project.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Core.Entity
{
	public class Leave:BaseEntity
	{
		public LeaveType LeaveType { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime RequestDate { get; set; }
		public DateTime? ResponseDate { get; set; }
		public int? NumberOfLeaveDays { get; set; }
		public int? UsedAnnualLeaveDays { get; set; }
		public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

		//---------------------------------------------------//

		public ICollection<Personel> Personels { get; set; }

		public Leave()
		{
			Personels = new List<Personel>();
		}
	}
}
