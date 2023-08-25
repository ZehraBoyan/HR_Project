using IK_Project.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Core.Repositories
{
	public interface IPersonelRepository:IGenericRepository<Personel>
	{
		Task<List<Personel>> GetPersonelWithCompany();	
	}
}
