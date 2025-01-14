using SympliSeoChecker.Common.Constants;
using SympliSeoChecker.Domain.Models.Responses;
using SympliSeoChecker.Service.Helpers;
using SympliSeoChecker.Service.Interfaces;
using System.Web;

namespace SympliSeoChecker.Service.SearchEngines
{
    public class GoogleSearchEngineService : IGoogleSearchEngineService
    {
        private readonly HttpClient _httpClient;

        public GoogleSearchEngineService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<RankingResponseModel>> GetSearchRankingAsync(string keyword, string url)
        {
            var keywordEncode = HttpUtility.UrlEncode(keyword);

            _httpClient.DefaultRequestHeaders.Remove("User-Agent");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);

            var htmlContent = await GetHtmlContentAsync(keywordEncode);
            var hrefs = SearchEngineHelper.GetHrefsFromHtmlContent(
                htmlContent,
                Constants.GoogleSearchHrefsPattern);

            return SearchEngineHelper.GetMatchedUrlRankings(hrefs, url);
        }

        #region private methods
        private async Task<string> GetHtmlContentAsync(string keyword)
        {
            var searchUrl = $"{Constants.GoogleSearchUrl}/search?q={keyword}&num={Constants.TotalSearchResultItems}";
            var response = await _httpClient.GetAsync(searchUrl, HttpCompletionOption.ResponseContentRead);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        #endregion
    }
}
