using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Web.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext actionContext)
        {
            var Errors = actionContext.ModelState.Where(M => M.Value.Errors.Any())
                            .Select(M => new ValidationError()
                            {
                                Field = M.Key,
                                Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                            });

            var response = new ValidationErrorToReturn()
            {
                validationErrors = Errors
            };

            return new BadRequestObjectResult(response);
        }
    }
}
