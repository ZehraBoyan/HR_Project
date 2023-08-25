using IK_Project.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Repository.Configurations
{
	public class LeaveConfiguration : IEntityTypeConfiguration<Leave>
	{
		public void Configure(EntityTypeBuilder<Leave> builder)
		{
			builder.ToTable("Leave");

			builder.HasKey(p => p.Id);

			builder.Property(p => p.LeaveType)
				.IsRequired();

			builder.Property(p => p.StartDate)
				.IsRequired();

			builder.Property(p => p.EndDate)
				.IsRequired();

			builder.Property(p => p.Status)
				.IsRequired();

			builder.Property(p => p.RequestDate)
				.IsRequired();

			builder.Property(p => p.ResponseDate)
				.IsRequired(false);

			builder.Property(p => p.NumberOfLeaveDays)
			.IsRequired(false);
			
			builder.Property(p => p.UsedAnnualLeaveDays).HasColumnType("decimal(18,2)")
			.IsRequired(false);

			//-----------------------------//

		
		}
	}
}
