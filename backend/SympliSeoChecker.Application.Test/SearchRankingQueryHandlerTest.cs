using FluentAssertions;
using NSubstitute;
using SympliSeoChecker.Application.Queries;
using SympliSeoChecker.Common.Enums;
using SympliSeoChecker.Domain.Models.Responses;
using SympliSeoChecker.Service.Caching;
using SympliSeoChecker.Service.Factories;
using SympliSeoChecker.Service.Interfaces;

namespace SympliSeoChecker.Application.Test
{
    [TestFixture]
    public class SearchRankingQueryHandlerTest
    {
        private ICachingService _cachingServiceMock;
        private ISearchEngineFactory _searchEngineFactoryMock;
        private SearchRankingQueryHandler _searchRankingQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _cachingServiceMock = Substitute.For<ICachingService>();
            _searchEngineFactoryMock = Substitute.For<ISearchEngineFactory>();
            _searchRankingQueryHandler = new SearchRankingQueryHandler(_searchEngineFactoryMock, _cachingServiceMock);
        }

        [Test]
        public async Task Handle_SearchRankingQuery_WhenCacheNotExist()
        {
            // Arrange
            var searchRankingQuery = new SearchRankingQuery(
                searchEngines: [(int)SearchEngineType.Google, (int)SearchEngineType.Bing],
                keyword: "wiktionary",
                url: "wiktionary.org");

            var googleCacheKey = "SearchRankingQueryHandler_searchEngineType:Google_keyword:wiktionary_url:wiktionary.org";
            var bingCacheKey = "SearchRankingQueryHandler_searchEngineType:Bing_keyword:wiktionary_url:wiktionary.org";

            IEnumerable<RankingResponseModel> googleSearchResult = new List<RankingResponseModel>
            {
                new()
                {
                    Rank = 1,
                    Url = "wiktionary.org"
                }
            };
            IEnumerable<RankingResponseModel> bingSearchResult = new List<RankingResponseModel>
            {
                new()
                {
                    Rank = 2,
                    Url = "wiktionary.org"
                }
            };

            _cachingServiceMock.TryGet(googleCacheKey, out Arg.Any<IEnumerable<RankingResponseModel>>()).Returns(false);
            _cachingServiceMock.TryGet(bingCacheKey, out Arg.Any<IEnumerable<RankingResponseModel>>()).Returns(false);

            var googleSearchEngineMock = Substitute.For<ISearchEngineService>();
            var bingSearchEngineMock = Substitute.For<ISearchEngineService>();

            googleSearchEngineMock.GetSearchRankingAsync(searchRankingQuery.Keyword, searchRankingQuery.Url).Returns(Task.FromResult(googleSearchResult));
            bingSearchEngineMock.GetSearchRankingAsync(searchRankingQuery.Keyword, searchRankingQuery.Url).Returns(Task.FromResult(bingSearchResult));

            _searchEngineFactoryMock.Create(SearchEngineType.Google).Returns(googleSearchEngineMock);
            _searchEngineFactoryMock.Create(SearchEngineType.Bing).Returns(bingSearchEngineMock);

            // Act
            var result = await _searchRankingQueryHandler.Handle(searchRankingQuery, CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result.First().SearchEngineName.Should().Be("Google");
            result.First().Rankings.Should().BeEquivalentTo(googleSearchResult);

            result.Last().SearchEngineName.Should().Be("Bing");
            result.Last().Rankings.Should().BeEquivalentTo(bingSearchResult);

            _searchEngineFactoryMock.Received(1).Create(SearchEngineType.Google);
            _searchEngineFactoryMock.Received(1).Create(SearchEngineType.Bing);
            _cachingServiceMock.Received(2);
        }

        [Test]
        public async Task Handle_SearchRankingQuery_WhenCacheExisting()
        {
            // Arrange
            var searchRankingQuery = new SearchRankingQuery(
                searchEngines: [(int)SearchEngineType.Google],
                keyword: "wiktionary",
                url: "wiktionary.org");

            var cacheKey = "SearchRankingQueryHandler_searchEngineType:Google_keyword:wiktionary_url:wiktionary.org";
            IEnumerable<RankingResponseModel> cachedValue = new List<RankingResponseModel>
            {
                new()
                {
                    Rank = 1,
                    Url = "wiktionary.org"
                }
            };

            _cachingServiceMock.TryGet(cacheKey, out Arg.Any<IEnumerable<RankingResponseModel>>()).Returns(x =>
            {
                x[1] = cachedValue;
                return true;
            });

            // Act
            var result = await _searchRankingQueryHandler.Handle(searchRankingQuery, CancellationToken.None);

            // Assert
            result.Should().HaveCount(1);
            result.First().SearchEngineName.Should().Be("Google");
            result.First().Rankings.Should().BeEquivalentTo(cachedValue);
            _searchEngineFactoryMock.DidNotReceive();
        }
    }
}
