using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Models
{
    public class CovidHttpClient
    {
        private HttpClientBase webClient;

        //Endpoints
        
       // private readonly string Covid = "https://covid-19-data.p.rapidapi.com/country?name=italy";

     
        private HttpClientBase _covidWebClient;

        // Endpoints of http://compass-core/
        private readonly string _contractEntryProgramInfoUri = "https://covid-19-data.p.rapidapi.com/country?name=italy";
      //  private readonly string _pricingProgramInfoUri = "api/contractentry/programlinks/bypricingprogram/{0}";
       // private readonly string _programAndPricingHistoryUri = "api/contractentry/program/{0}/programandpricinghistory";
       // private readonly string _userInfoUri = "api/Users/{0}";

 

        public CovidHttpClient(string baseUri, int timeoutSeconds = 300)
        {
            _covidWebClient = new HttpClientBase(new HttpClient() { BaseAddress = new Uri(baseUri),
                DefaultRequestHeaders =
    {
        { "x-rapidapi-host", "covid-19-data.p.rapidapi.com" },
        { "x-rapidapi-key", "66129cc143mshe51dabf480f766ep1cd41djsn4eaca4ce5563" },
    }, Timeout = TimeSpan.FromSeconds(timeoutSeconds) });
        }

        // ----------------------------------------------------------------------------------------
        // TEST_ONLY: Constructor for testing which allows us to override the HttpClient
        public CovidHttpClient(HttpClient test)
        {
            _covidWebClient = new HttpClientBase(test);
        }

       

      //  public async Task<CompassContractEntryProgram> GetContractEntryProgramInfo(int contractEntryProgramId) =>
       //     await _compassWebClient.Get<CompassContractEntryProgram>(string.Format(_contractEntryProgramInfoUri, contractEntryProgramId));

       

        public Info GetInfo(string country) =>
            _covidWebClient.Get<Info>(string.Format(_contractEntryProgramInfoUri,country)).Result;

      


    }
}
