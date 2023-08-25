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
    public class PersonelConfiguration : IEntityTypeConfiguration<Personel>
    {
        public void Configure(EntityTypeBuilder<Personel> builder)
        {
            builder.ToTable("Personel");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Photo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.MiddleName)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.SecondLastName)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(p => p.BirthDate)
                .IsRequired();

            builder.Property(p => p.BirthPlace)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.TcIdentityNo)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(p => p.HireDate)
                .IsRequired();

            builder.Property(p => p.DismissalDate)
                .IsRequired(false);

            builder.Property(p => p.CompanyName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Department)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Occupation)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(10);

            builder.Property(p => p.Salary)
                .IsRequired();

            builder.Property(p => p.Email)
                .IsRequired(false)
                .HasMaxLength(255);

			builder.Property(p => p.Gender)
				.IsRequired();

			builder.Property(p => p.Password)
				.IsRequired(false);

			builder.Property(p => p.UsedAnnualLeaveDays)
				.IsRequired(false);

			//------------------------------//

			builder.HasOne(b => b.Company).WithMany(p=>p.Personels).HasForeignKey(c=>c.CompanyId);
			builder.HasMany(b => b.Expenses);
			builder.HasMany(b => b.Leaves);
		}        
    }
}
