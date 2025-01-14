using FluentValidation;
using SympliSeoChecker.Application.Queries;
using SympliSeoChecker.Common.Constants;
using SympliSeoChecker.Common.Enums;
using SympliSeoChecker.Common.Extensions;
using SympliSeoChecker.Common.Utilities;

namespace SympliSeoChecker.Application.Validators
{
    public class SearchRankingQueryValidator : AbstractValidator<SearchRankingQuery>
    {
        public SearchRankingQueryValidator()
        {
            // validate search engine
            RuleFor(x => x.SearchEngines)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                    .WithErrorCode(ErrorCode.SearchEngineIsRequired.ToNumberString())
                    .WithMessage(CommonUtility.GetErrorMessage(ErrorCode.SearchEngineIsRequired));

            // validate search engine type item
            RuleForEach(x => x.SearchEngines)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithErrorCode(ErrorCode.SearchEngineTypeIsRequired.ToNumberString())
                    .WithMessage(CommonUtility.GetErrorMessage(ErrorCode.SearchEngineTypeIsRequired))
                .Must(CommonUtility.IsValidEnum<SearchEngineType>)
                    .WithErrorCode(ErrorCode.SearchEngineTypeIsInRange.ToNumberString())
                    .WithMessage(CommonUtility.GetErrorMessage(ErrorCode.SearchEngineTypeIsInRange));

            // validate keyword
            RuleFor(x => x.Keyword)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithErrorCode(ErrorCode.KeywordIsRequired.ToNumberString())
                    .WithMessage(CommonUtility.GetErrorMessage(ErrorCode.KeywordIsRequired))
                .NotEmpty()
                    .WithErrorCode(ErrorCode.KeywordIsNotEmpty.ToNumberString())
                    .WithMessage(CommonUtility.GetErrorMessage(ErrorCode.KeywordIsNotEmpty))
                .MinimumLength(Constants.StringMinLength)
                    .WithErrorCode(ErrorCode.KeywordIsGreaterThan.ToNumberString())
                    .WithMessage(CommonUtility.GetErrorMessage(ErrorCode.KeywordIsGreaterThan))
                .MaximumLength(Constants.StringMaxLength)
                    .WithErrorCode(ErrorCode.KeywordIsLessThan.ToNumberString())
                    .WithMessage(CommonUtility.GetErrorMessage(ErrorCode.KeywordIsLessThan));

            // validate url
            RuleFor(x => x.Url)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithErrorCode(ErrorCode.UrlIsRequired.ToNumberString())
                    .WithMessage(CommonUtility.GetErrorMessage(ErrorCode.UrlIsRequired))
                .NotEmpty()
                    .WithErrorCode(ErrorCode.UrlIsNotEmpty.ToNumberString())
                    .WithMessage(CommonUtility.GetErrorMessage(ErrorCode.UrlIsNotEmpty))
                .Must(CommonUtility.IsValidUrl)
                    .WithErrorCode(ErrorCode.UrlIsInvalid.ToNumberString())
                    .WithMessage(CommonUtility.GetErrorMessage(ErrorCode.UrlIsInvalid));
        }
    }
}
