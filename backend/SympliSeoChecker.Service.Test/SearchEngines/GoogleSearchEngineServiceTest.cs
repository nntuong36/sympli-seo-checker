using FluentAssertions;
using SympliSeoChecker.Domain.Models.Responses;
using SympliSeoChecker.Service.SearchEngines;
using SympliSeoChecker.Service.Test.HttpMessageHandlers;
using System.Net;

namespace SympliSeoChecker.Service.Test.SearchEngines
{
    public class GoogleSearchEngineServiceTest
    {
        private MockHttpMessageHandler _mockHttpMessageHandler;
        private HttpClient _httpClient;
        private GoogleSearchEngineService _googleSearchEngineService;

        [SetUp]
        public void SetUp()
        {
            _mockHttpMessageHandler = new MockHttpMessageHandler();
            _httpClient = new HttpClient(_mockHttpMessageHandler);
            _googleSearchEngineService = new GoogleSearchEngineService(_httpClient);
        }

        [Test]
        public async Task SearchAsync_ReturnSearchRanking_FoundManyTimes()
        {
            // Arrange
            string keyword = "E-Settlement";
            string url = "https://www.gov.uk/";

            // Set up mock responses
            _mockHttpMessageHandler.SetupResponse(
                    $"https://www.google.com.au/search?q={keyword}&num=100",
                    HttpStatusCode.OK,
                    content: File.ReadAllText($"TestData/google_100items.html")
                );

            // Act
            var result = await _googleSearchEngineService.GetSearchRankingAsync(keyword, url);

            var expectedResult = new List<RankingResponseModel>()
            {
                new()
                {
                    Url = "https://www.gov.uk/settled-status-eu-citizens-families",
                    Rank = 3,
                },
                new()
                {
                    Url = "https://www.gov.uk/view-prove-immigration-status",
                    Rank = 58,
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

            // Set up mock response
            _mockHttpMessageHandler.SetupResponse(
                    $"https://www.google.com.au/search?q={keyword}&num=100",
                    HttpStatusCode.OK,
                    content: File.ReadAllText($"TestData/google_0item.html")
                );

            // Act
            var result = await _googleSearchEngineService.GetSearchRankingAsync(keyword, url);

            var expectedResult = new List<RankingResponseModel>();

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
