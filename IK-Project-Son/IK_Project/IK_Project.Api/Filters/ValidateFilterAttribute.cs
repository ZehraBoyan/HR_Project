using IK_Project.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static IK_Project.Core.DTOs.NoContentDto;

namespace IK_Project.Api.Filters
{
	public class ValidateFilterAttribute:ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x=>x.ErrorMessage).ToList();

				context.Result = new BadRequestObjectResult(CustomResponseDTO<NoContentDto>.Fail(400, errors));

			
			}
		}
	}
}
