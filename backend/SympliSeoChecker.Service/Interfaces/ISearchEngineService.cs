using SympliSeoChecker.Domain.Models.Responses;

namespace SympliSeoChecker.Service.Interfaces
{
    public interface ISearchEngineService
    {
        Task<IEnumerable<RankingResponseModel>> GetSearchRankingAsync(string keyword, string url);
    }
}
