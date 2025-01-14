using SympliSeoChecker.Common.Enums;
using SympliSeoChecker.Service.Interfaces;

namespace SympliSeoChecker.Service.Factories
{
    public interface ISearchEngineFactory
    {
        ISearchEngineService Create(SearchEngineType searchEngineType);
    }
}
