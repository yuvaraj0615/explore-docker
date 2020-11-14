using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _clientFactory;
        public IEnumerable<WeatherForecast> Branches { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task OnGet()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://webapi/weatherforecast");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                Branches = await JsonSerializer.DeserializeAsync
                    <IEnumerable<WeatherForecast>>(responseStream);
            }
            else
            {
                //GetBranchesError = true;
                Branches = Array.Empty<WeatherForecast>();
            }
        }
    }
}
