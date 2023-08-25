using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using IK_Project.Api.Filters;
using IK_Project.Api.Filters.Middlewares;
using IK_Project.Core.DTOs;
using IK_Project.Core.Repositories;
using IK_Project.Core.Services;
using IK_Project.Core.UnitOfWorks;
using IK_Project.Repository.Context;
using IK_Project.Repository.Repositories;
using IK_Project.Repository.UnitOfWork;
using IK_Project.Service.Mapping;
using IK_Project.Service.Services;
using IK_Project.Service.Validations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IK_Project.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers(options =>options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<Program>());
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddScoped(typeof(NotFoundFilter<>));

			builder.Services.AddScoped<IUnitOfWorks,UnitOfWork>();
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

			builder.Services.AddScoped<IPersonelRepository, PersonelRepository>();
			builder.Services.AddScoped<IPersonelService, PersonelService>();

			builder.Services.AddScoped<ICompanyService, CompanyService>();
			builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

			builder.Services.AddAutoMapper(typeof(MapProfile));

			builder.Services.AddDbContext<AppDbContext>(x =>
			{
				x.UseSqlServer(builder.Configuration.GetConnectionString("Con"), option =>
				{
					option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
				});
			});

			//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
			//builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseCustomException();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}