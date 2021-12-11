using DeveloperTest.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTest.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult MapResponse<T>(ApplicationResult<T> result) where T : class
        {
            return result.GetStatusCode() switch
            {
                Domain.StatusCode.Success => Ok(result),
                Domain.StatusCode.BadRequest => BadRequest(result),
                Domain.StatusCode.Unauthorized => Unauthorized(result),
                Domain.StatusCode.Created => Created(string.Empty, result),
                Domain.StatusCode.NoContent => NoContent(),
                _ => StatusCode(StatusCodes.Status500InternalServerError, result)
            };
        }
    }
}
