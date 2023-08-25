﻿using IK_Project.Core.DTOs;
using IK_Project.Core.Entity;
using IK_Project.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IK_Project.Api.Filters
{
	public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
	{
		private readonly IService<T> _service;
		public NotFoundFilter(IService<T> service)
		{
			_service = service;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var idValue = context.ActionArguments.Values.FirstOrDefault();
			if (idValue == null)
			{
				await next.Invoke();
				return;
			}
			var id=(int)idValue;
			var anyEntity= await _service.AnyAsync(x=>x.Id==id);

			if (anyEntity)
			{
				await next.Invoke();
				return;
			}
			context.Result = new NotFoundObjectResult(CustomResponseDTO<NoContentDto>.Fail(404, $"{typeof(T).Name}({id})not found"));


		}
	}
}
