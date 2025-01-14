using SympliSeoChecker.Common.Constants;
using SympliSeoChecker.Domain.Models.Responses;
using System.Text.RegularExpressions;

namespace SympliSeoChecker.Service.Helpers
{
    public static class SearchEngineHelper
    {
        public static List<RankingResponseModel> GetMatchedUrlRankings(IEnumerable<string> hrefs, string url)
        {
            return hrefs
                .Select((href, index) => (href, ranking: index + 1))
                .Where(x => x.href.Contains(url, StringComparison.OrdinalIgnoreCase))
                .Select(x => new RankingResponseModel()
                {
                    Url = x.href,
                    Rank = x.ranking
                })
                .ToList();
        }

        public static List<string> GetHrefsFromHtmlContent(string htmlContent, string pattern)
        {
            Regex regex = new(pattern, RegexOptions.Singleline);
            var matches = regex.Matches(htmlContent);
            var topMatches = matches.Cast<Match>().Take(Constants.TotalSearchResultItems);

            var hrefs = new List<string>();
            foreach (var match in topMatches)
            {
                string href = match.Groups[1].Value;
                hrefs.Add(href);
            }

            return hrefs;
        }
    }
}
