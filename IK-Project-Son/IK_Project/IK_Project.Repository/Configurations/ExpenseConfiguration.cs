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
	public class ExpenseConfiguration:IEntityTypeConfiguration<ExpenseRequest>
	{
		public void Configure(EntityTypeBuilder<ExpenseRequest> builder)
		{
			builder.ToTable("ExpenseRequest");

			builder.HasKey(p => p.Id);

			builder.Property(p => p.ExpenseType)
				.IsRequired();

			builder.Property(p => p.SpentAmount)
				.IsRequired();

			builder.Property(p => p.Currency)
				.IsRequired();

			builder.Property(p => p.ApprovalStatus)
				.IsRequired();

			builder.Property(p => p.RequestDate)
				.IsRequired();

			builder.Property(p => p.ResponseDate)
				.IsRequired(false);

			builder.Property(p => p.DocumentPath)
			.IsRequired();

			//-----------------------------//

			
		
		}
	}
}

