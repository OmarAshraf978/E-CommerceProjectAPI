using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Shared.ResultPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        //Handle Result Without Value
        protected IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
                return NoContent();
            else
                return HandleProblem(result.Errors);
        }
        //Handle Result With Value
        protected ActionResult<TValue> HandleResult<TValue>(Result<TValue> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return HandleProblem(result.Errors);
        }

        private ActionResult HandleProblem(IReadOnlyList<Error> errors)
        {
            if (errors.Count == 0)
                return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An Unexpected Error Occurred");
            if (errors.All(e => e.Type == ErrorTypes.Validation))
                return HandleValidationProblem(errors);
            return HandleSingleResultProblem(errors[0]);
        }

        private ActionResult HandleSingleResultProblem(Error error)
        {
            return Problem
                   (
                      title: error.Code,
                      detail: error.Description,
                      type: error.Type.ToString(),
                      statusCode: MapErrorTypeToStatusCode(error.Type)
                   );
        }

        private ActionResult HandleValidationProblem(IReadOnlyList<Error> errors)
        {
            var ModelState = new ModelStateDictionary();
            foreach (var  error in errors)
                ModelState.AddModelError(error.Code, error.Description);
            return ValidationProblem(ModelState);
        }

        private static int MapErrorTypeToStatusCode(ErrorTypes errorTypes) => errorTypes switch
        {
            ErrorTypes.NotFound => StatusCodes.Status404NotFound,
            ErrorTypes.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorTypes.Forbidden => StatusCodes.Status403Forbidden,
            ErrorTypes.Validation => StatusCodes.Status400BadRequest,
            ErrorTypes.InvalidCrendentials => StatusCodes.Status401Unauthorized,
            ErrorTypes.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
