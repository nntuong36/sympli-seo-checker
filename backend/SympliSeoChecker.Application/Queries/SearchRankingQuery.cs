using MediatR;
using SympliSeoChecker.Domain.Models.Responses;

namespace SympliSeoChecker.Application.Queries
{
    public class SearchRankingQuery : IRequest<List<SearchRankingResponseModel>>
    {
        public SearchRankingQuery(
            IEnumerable<int?> searchEngines,
            string? keyword,
            string? url)
        {
            SearchEngines = searchEngines;
            Keyword = keyword;
            Url = url;
        }

        public string? Keyword { get; set; }
        public string? Url { get; set; }
        public IEnumerable<int?> SearchEngines { get; set; }
    }
}
