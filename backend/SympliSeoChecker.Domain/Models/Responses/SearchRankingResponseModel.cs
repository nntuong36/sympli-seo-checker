using SympliSeoChecker.Common.Enums;

namespace SympliSeoChecker.Domain.Models.Responses
{
    public class SearchRankingResponseModel
    {
        public SearchEngineType SearchEngine { get; set; }
        public string SearchEngineName { get; set; }

        public IEnumerable<RankingResponseModel> Rankings { get; set; }
    }
}
