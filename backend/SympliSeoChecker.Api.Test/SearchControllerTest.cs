using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SympliSeoChecker.Api.Controllers;
using SympliSeoChecker.Application.Queries;
using SympliSeoChecker.Common.Enums;
using SympliSeoChecker.Domain.Models.Responses;

namespace SympliSeoChecker.Api.Test
{
    [TestFixture]
    public class SearchControllerTest
    {
        private IMediator _mediatorMock;
        private SearchController _searchController;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _searchController = new SearchController(_mediatorMock);
        }

        [Test]
        public async Task Search_ReturnOk_WhenValidRequest()
        {
            // Arrange
            var searchEngines = new List<int?> { (int)SearchEngineType.Google, (int)SearchEngineType.Bing };
            var keyword = "wiktionary";
            var url = "wiktionary.org";

            var expectedResult = new List<SearchRankingResponseModel>()
            {
                new ()
                {
                    SearchEngine = SearchEngineType.Google,
                    SearchEngineName = "Google",
                    Rankings = new List<RankingResponseModel>
                    {
                        new()
                        {
                            Rank = 1,
                            Url = "https://www.wiktionary.org/"
                        }
                    }
                },
                new ()
                {
                    SearchEngine = SearchEngineType.Bing,
                    SearchEngineName = "Bing",
                    Rankings = new List<RankingResponseModel>
                    {
                        new()
                        {
                            Rank = 2,
                            Url = "https://www.wiktionary.org/"
                        }
                    }
                }
            };

            _mediatorMock
                .Send(Arg.Any<SearchRankingQuery>(), Arg.Any<CancellationToken>())
                .Returns(expectedResult);

            // Act
            var result = await _searchController.GetRanking(searchEngines, keyword, url);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().BeEquivalentTo(expectedResult);
        }
    }
}
