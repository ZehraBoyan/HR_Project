using IK_Project.Core.Entity;
using IK_Project.Core.Repositories;
using IK_Project.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Repository.Repositories
{
	public class PersonelRepository : GenericRepository<Personel>, IPersonelRepository
	{
		public PersonelRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<List<Personel>> GetPersonelWithCompany()
		{
			return await _context.Personels.Include(x => x.Company).ToListAsync();
		}

	}
}
