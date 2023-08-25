using IK_Project.Core.UnitOfWorks;
using IK_Project.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Repository.UnitOfWork
{
	public class UnitOfWork: IUnitOfWorks
	{
		private readonly AppDbContext _context;
		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		public void Commit()
		{
			_context.SaveChanges();
		}
		public async Task CommitAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
