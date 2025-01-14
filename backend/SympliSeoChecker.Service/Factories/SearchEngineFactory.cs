using Microsoft.Extensions.DependencyInjection;
using SympliSeoChecker.Common.Enums;
using SympliSeoChecker.Service.Interfaces;

namespace SympliSeoChecker.Service.Factories
{
    public class SearchEngineFactory : ISearchEngineFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SearchEngineFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public ISearchEngineService Create(SearchEngineType searchEngineType)
        {
            return searchEngineType switch
            {
                SearchEngineType.Google => _serviceProvider.GetRequiredService<IGoogleSearchEngineService>(),
                SearchEngineType.Bing => _serviceProvider.GetRequiredService<IBingSearchEngineService>(),
                _ => throw new ArgumentException("Search engine type is not support")
            };
        }
    }
}
