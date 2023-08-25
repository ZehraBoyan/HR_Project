using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Core.Entity
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
		public bool IsActive { get; set; }

		public DateTime CreatedDate {
            get
            {
                return DateTime.Now;
            }
            }
        public DateTime? UpdatedDate
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
