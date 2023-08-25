using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Core.UnitOfWorks
{
	public interface IUnitOfWorks
	{
		//Save changes e gerek kalmadan db ye kayıt yapmış oluyorum.
		Task CommitAsync();
		void Commit();
	}
}
