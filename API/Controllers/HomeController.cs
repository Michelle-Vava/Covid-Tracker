using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CovidHttpClient _data;
        //private static JsonSerializerOptions _jsonOptions => new() { WriteIndeted = true };
        public HomeController(ILogger<HomeController> logger)
        {
            
            _logger = logger;
        }


        //TODO : make endpoint a class and select covid data based on any country
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-19-data.p.rapidapi.com/country?name=italy&format=json"),
                Headers =
            {
                { "x-rapidapi-host", "covid-19-data.p.rapidapi.com" },
                { "x-rapidapi-key", "66129cc143mshe51dabf480f766ep1cd41djsn4eaca4ce5563" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return new ObjectResult(body);
            }
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
