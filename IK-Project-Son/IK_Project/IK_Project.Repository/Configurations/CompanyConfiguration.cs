using IK_Project.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Repository.Configurations
{
	public class CompanyConfiguration : IEntityTypeConfiguration<Company>
	{
		public void Configure(EntityTypeBuilder<Company> builder)
		{
			builder.ToTable("Company");

			builder.HasKey(x => x.Id);

			builder.Property(p => p.Name)
			   .IsRequired()
			   .HasMaxLength(50);

			builder.Property(p => p.Title)
			   .IsRequired()
			   .HasMaxLength(50);

			builder.Property(p => p.MersisNumber)
			   .IsRequired()
			   .HasMaxLength(50);


			builder.Property(p => p.TaxNumber)
			   .IsRequired()
			   .HasMaxLength(50);

			builder.Property(p => p.TaxAdministration)
			   .IsRequired()
			   .HasMaxLength(50);

			builder.Property(p => p.Logo)
			   .IsRequired(false)
			   .HasMaxLength(50);

			builder.Property(p => p.PhoneNumber)
			   .HasMaxLength(50);

			builder.Property(p => p.Address)
			   .IsRequired()
			   .HasMaxLength(50); 
			
			builder.Property(p => p.Email)
			   .IsRequired()
			   .HasMaxLength(50); 
			
			builder.Property(p => p.NumberOfEmployees)
			   .IsRequired(false)
			   .HasMaxLength(50); 
			
			builder.Property(p => p.FoundationYear)
			   .IsRequired()
			   .HasMaxLength(50);

			builder.Property(p => p.ContractStart)
			   .IsRequired()
			   .HasMaxLength(50);

			builder.Property(p => p.ContractEnd)
			   .IsRequired()
			   .HasMaxLength(50);

			//--------------------------------------------------------------------//

			builder.HasMany(b => b.Personels);
		}
	}
}
