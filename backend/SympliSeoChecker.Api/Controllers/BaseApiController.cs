using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SympliSeoChecker.Domain.Models.Responses;

namespace SympliSeoChecker.Api.Controllers
{
    [SwaggerResponse(400, type: typeof(ErrorResponseModel))]
    [SwaggerResponse(500, type: typeof(ErrorResponseModel))]
    public class BaseApiController : ControllerBase
    {
        public BaseApiController()
        {

        }
    }
}
