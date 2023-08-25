using IK_Project.Core.DTOs;
using IK_Project.Core.Entity;
using IK_Project.Repository.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Repository.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //var m = new Personel() { Manager = new Manager() };
        }
        public DbSet<Personel> Personels { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ExpenseRequest> ExpenseRequests { get; set; }
        public DbSet<Leave> Leaves { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            // Butun configurasyonları bulup işlesin kod kalabalığı olmasın !!
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}
	}
}
