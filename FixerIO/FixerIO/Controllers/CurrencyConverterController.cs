using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace FixerIO.Controllers
{
    [ApiController]
    public class CurrencyConverterController : ControllerBase
    {
        private readonly ILogger<CurrencyConverterController> _logger;
        private readonly IConfiguration _config;

        public CurrencyConverterController(
            ILogger<CurrencyConverterController> logger,
            IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet("{fromCurr}/{fromCurrAmt}/{toCurr}")]
        public ActionResult Get(string fromCurr, int fromCurrAmt, string toCurr)
        {
            var baseURL = _config["FixerIo:BaseURL"];
            var apiKey = _config["FixerIo:ApiKey"];

            _logger.LogTrace($"API called to convert {fromCurrAmt} {fromCurr} into {toCurr}");

            if (!(fromCurr.Length == 3 && toCurr.Length == 3 && fromCurrAmt > 0))
            {
                return BadRequest("Incorrect request, currencies should have 3 letters and conversion amount should be greater than 0");
            }

            var finalURL = baseURL + $"convert?access_key={apiKey}&from={fromCurr}&to={toCurr}&amount={fromCurrAmt}";
            // sample URL : https://api.apilayer.com/fixer/convert?to=INR&from=USD&amount=1

            var client = new RestClient(baseURL);

            var request = new RestRequest("convert");

            request.AddParameter("from", fromCurr);
            request.AddParameter("to", toCurr);
            request.AddParameter("amount", fromCurrAmt);

            request.AddHeader("apikey", "Pa0VHtUTOpyNtlZmemdELbjJM48vkvhy");

            var response = client.ExecuteGet(request);

            return Ok(response.Content);
        }
    }
}