using MediatR;
using SympliSeoChecker.Common.Constants;
using SympliSeoChecker.Common.Enums;
using SympliSeoChecker.Common.Extensions;
using SympliSeoChecker.Domain.Models.Responses;
using SympliSeoChecker.Service.Caching;
using SympliSeoChecker.Service.Factories;

namespace SympliSeoChecker.Application.Queries
{
    public class SearchRankingQueryHandler : IRequestHandler<SearchRankingQuery, List<SearchRankingResponseModel>>
    {
        private readonly ISearchEngineFactory _searchEngineFactory;
        private readonly ICachingService _cachingService;
        public SearchRankingQueryHandler(
            ISearchEngineFactory searchEngineFactory,
            ICachingService cachingService)
        {
            _searchEngineFactory = searchEngineFactory;
            _cachingService = cachingService;
        }

        public async Task<List<SearchRankingResponseModel>> Handle(
            SearchRankingQuery request,
            CancellationToken cancellationToken)
        {
            var result = new List<SearchRankingResponseModel>();

            if (request.Keyword is null || request.Url is null) return result;

            foreach (var searchEngine in request.SearchEngines)
            {
                if (searchEngine is null) continue;
                var searchEngineType = (SearchEngineType)searchEngine;

                // cache handle
                var cacheKey =
                    $"{nameof(SearchRankingQueryHandler)}" +
                    $"_searchEngineType:{searchEngineType}" +
                    $"_keyword:{request.Keyword}" +
                    $"_url:{request.Url}";

                if (!_cachingService.TryGet(cacheKey, out IEnumerable<RankingResponseModel> rankingCacheValuues))
                {
                    // no existing in cache => get new value from service
                    var searchEngineFactory = _searchEngineFactory.Create(searchEngineType);
                    var searchRankings = await searchEngineFactory.GetSearchRankingAsync(request.Keyword, request.Url);

                    // update new value to cache
                    rankingCacheValuues = searchRankings;
                    _cachingService.Set(
                        cacheKey,
                        rankingCacheValuues,
                        TimeSpan.FromSeconds(Constants.CacheExpirationInSecond));
                }

                // add to response result
                result.Add(new SearchRankingResponseModel()
                {
                    SearchEngine = searchEngineType,
                    SearchEngineName = searchEngineType.GetEnumDescription(),
                    Rankings = rankingCacheValuues
                });
            }

            return result;
        }
    }
}
