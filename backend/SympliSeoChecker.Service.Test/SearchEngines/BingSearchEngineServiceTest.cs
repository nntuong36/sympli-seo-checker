using FluentAssertions;
using SympliSeoChecker.Domain.Models.Responses;
using SympliSeoChecker.Service.SearchEngines;
using SympliSeoChecker.Service.Test.HttpMessageHandlers;
using System.Net;

namespace SympliSeoChecker.Service.Test.SearchEngines
{
    public class BingSearchEngineServiceTest
    {
        private MockHttpMessageHandler _mockHttpMessageHandler;
        private HttpClient _httpClient;
        private BingSearchEngineService _bingSearchEngineService;

        [SetUp]
        public void SetUp()
        {
            _mockHttpMessageHandler = new MockHttpMessageHandler();
            _httpClient = new HttpClient(_mockHttpMessageHandler);
            _bingSearchEngineService = new BingSearchEngineService(_httpClient);
        }

        [Test]
        public async Task SearchAsync_ReturnSearchRanking_FoundManyTimes()
        {
            // Arrange
            string keywords = "E-Settlement";
            string url = "https://www.gov.uk/";

            // Set up mock responses for 10 pages of search results
            for (int i = 0; i < 10; i++)
            {
                _mockHttpMessageHandler.SetupResponse(
                    $"https://www.bing.com/search?q={keywords}&first={i * 10 + 1}",
                    HttpStatusCode.OK,
                    content: File.ReadAllText($"TestData/bing_page{i}.html")
                );
            }

            // Act
            var result = await _bingSearchEngineService.GetSearchRankingAsync(keywords, url);

            var expectedResult = new List<RankingResponseModel>()
            {
                new()
                {
                    Url = "https://www.gov.uk/view-prove-immigration-status",
                    Rank = 2,
                },
                new()
                {
                    Url = "https://www.gov.uk/settled-status-eu-citizens-families/applying-for-settled-status",
                    Rank = 3,
                },
                new()
                {
                    Url = "https://www.gov.uk/update-uk-visas-immigration-account-details",
                    Rank = 49,
                },
                new()
                {
                    Url = "https://www.gov.uk/settled-status-eu-citizens-families",
                    Rank = 53,
                },
                new()
                {
                    Url = "https://www.gov.uk/check-immigration-status",
                    Rank = 87,
                }
            };

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task SearchAsync_ReturnSearchRanking_EmptyResponse()
        {
            // Arrange
            string keyword = "wieuiwqawpaq";
            string url = "https://www.gov.uk/";

            // Set up mock responses
            for (int i = 0; i < 10; i++)
            {
                _mockHttpMessageHandler.SetupResponse(
                    $"https://www.bing.com/search?q={keyword}&first={i * 10 + 1}",
                    HttpStatusCode.OK,
                    content: File.ReadAllText($"TestData/bing_0item.html")
                );
            }

            // Act
            var result = await _bingSearchEngineService.GetSearchRankingAsync(keyword, url);

            var expectedResult = new List<RankingResponseModel>();

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
