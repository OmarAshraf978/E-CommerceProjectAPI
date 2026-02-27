using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Factories
{
    #region ApiResponseFactory
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext actionContext)
        {
            var Errors = actionContext.ModelState.Where(E => E.Value.Errors.Count > 0)
                         .ToDictionary(K => K.Key, V => V.Value.Errors.Select(E => E.ErrorMessage).ToArray());
            var Problem = new ProblemDetails()
            {
                Title = "Validation Errors",
                Detail = "One or more validation errors occurred",
                Status = StatusCodes.Status400BadRequest,
                Extensions = { { "Errors", Errors } }
            };
            return new BadRequestObjectResult(Problem);
        }
    }
    #endregion
}
