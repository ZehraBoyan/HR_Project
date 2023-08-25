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
	public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
	{
		public CompanyRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<Company> GetSingleCompanyByIdWithPersonelAsync(int companyId)
		{
			return await _context.Companies.Include(x => x.Personels).Where(x => x.Id == companyId).SingleOrDefaultAsync();
		}
	}
}
