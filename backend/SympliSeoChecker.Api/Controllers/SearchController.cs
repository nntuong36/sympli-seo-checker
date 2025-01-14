using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SympliSeoChecker.Application.Queries;
using SympliSeoChecker.Domain.Models.Responses;

namespace SympliSeoChecker.Api.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : BaseApiController
    {
        private readonly IMediator _mediator;

        public SearchController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ranking")]
        [SwaggerResponse(200, type: typeof(SearchRankingResponseModel))]
        public async Task<IActionResult> GetRanking(
            [FromQuery] IEnumerable<int?> searchEngines,
            [FromQuery] string? keyword,
            [FromQuery] string? url)
        {
            // init request model
            var queryRequest = new SearchRankingQuery(searchEngines, keyword, url);

            // get ranking result
            var result = await _mediator.Send(queryRequest);

            return Ok(result);
        }
    }
}
