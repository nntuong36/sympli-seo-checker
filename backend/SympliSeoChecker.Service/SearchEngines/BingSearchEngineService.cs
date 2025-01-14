using SympliSeoChecker.Common.Constants;
using SympliSeoChecker.Common.Utilities;
using SympliSeoChecker.Domain.Models.Responses;
using SympliSeoChecker.Service.Helpers;
using SympliSeoChecker.Service.Interfaces;
using System.Text;

namespace SympliSeoChecker.Service.SearchEngines
{
    public class BingSearchEngineService : IBingSearchEngineService
    {
        private readonly HttpClient _httpClient;

        public BingSearchEngineService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<RankingResponseModel>> GetSearchRankingAsync(string keyword, string url)
        {
            var keywordEncode = Uri.EscapeDataString(keyword);

            _httpClient.DefaultRequestHeaders.Remove("User-Agent");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);

            var htmlContent = await GetHtmlContentAsync(keywordEncode);
            var hrefs = SearchEngineHelper.GetHrefsFromHtmlContent(
                htmlContent,
                Constants.BingSearchHrefsPattern);

            return SearchEngineHelper.GetMatchedUrlRankings(hrefs, url);
        }

        #region private methods
        private async Task<string> GetHtmlContentAsync(string keyword)
        {
            var resultContent = new StringBuilder();
            var random = new Random();

            var totalPage = CommonUtility.CalcPagesCount(
                Constants.TotalSearchResultItems,
                Constants.BingSearchMaxItemPerPage);
            for (int pageIndex = 0; pageIndex < totalPage; pageIndex++)
            {
                await Task.Delay(random.Next(0, 300)); // random delay time to prevent considering as bot

                var htmlContent = await GetHtmlContentByPageIndexAsync(keyword, pageIndex);
                if (!string.IsNullOrEmpty(htmlContent))
                {
                    resultContent.Append(htmlContent);
                }
            }

            return resultContent.ToString();
        }

        private async Task<string> GetHtmlContentByPageIndexAsync(string keyword, int pageIndex)
        {
            var searchUrl = $"{Constants.BingSearchUrl}/search?q={keyword}&first={pageIndex * Constants.BingSearchMaxItemPerPage + 1}";
            var response = await _httpClient.GetAsync(searchUrl);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        #endregion
    }
}
