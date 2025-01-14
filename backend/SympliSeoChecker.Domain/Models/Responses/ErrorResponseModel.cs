using Newtonsoft.Json;
using SympliSeoChecker.Domain.Models.Validators;

namespace SympliSeoChecker.Domain.Models.Responses
{
    public interface IErrorResponseModel
    {

    }

    public class ErrorResponseModel : IErrorResponseModel
    {
        [JsonProperty("code", Order = 0)]
        public long Code { get; set; }

        [JsonProperty("message", Order = 1)]
        public string Message { get; set; }

        [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore, Order = 2)]
        public List<ValidateErrorModel> Details { get; set; }
    }
}
