namespace SympliSeoChecker.Common.Enums
{
    public enum ErrorCode : long
    {
        // common error code
        Invalid = 1001,

        // search engine error code
        SearchEngineIsRequired = 2001,
        SearchEngineTypeIsRequired = 2002,
        SearchEngineTypeIsInRange = 2003,

        // keyword error code
        KeywordIsRequired = 3001,
        KeywordIsNotEmpty = 3002,
        KeywordIsGreaterThan = 3003,
        KeywordIsLessThan = 3004,

        // url error code
        UrlIsRequired = 4001,
        UrlIsNotEmpty = 4002,
        UrlIsInvalid = 4003,
    }
}
