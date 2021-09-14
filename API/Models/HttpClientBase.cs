using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace API.Models
{
    public class HttpClientBase
    {
        public TimeSpan ClientTimeOut { get; set; }

        private HttpClient _client;

        private static MediaTypeWithQualityHeaderValue applicationJson => new MediaTypeWithQualityHeaderValue("application/json");
        private static JsonMediaTypeFormatter jsonFormatter => new JsonMediaTypeFormatter
        {
            SerializerSettings = {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Auto
            }
        };

        // ----------------------------------------------------------------------------------------
        // Constructor for general purposes.
        // The 'baseUri' is expected to be 'https://abacus/' or 'https://abacus-dev/'
        public HttpClientBase(Uri baseUri, int timeoutSeconds = 300)
        {
            _client = new HttpClient() { BaseAddress = baseUri, Timeout = TimeSpan.FromSeconds(timeoutSeconds) };
            _InitializeHttpClient();
        }

        // ----------------------------------------------------------------------------------------
        // TEST_ONLY: Constructor for testing which allows us to override the HttpClient
        public HttpClientBase(HttpClient client)
        {
            _client = client;
            _InitializeHttpClient();
        }

        // ----------------------------------------------------------------------------------------
        // Private helper to initilize the underlying HttpClient used to make the requests.
        private void _InitializeHttpClient()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(applicationJson);
        }

        // ----------------------------------------------------------------------------------------
        // Send a GET request to the Abacus server.
        // Returns a 'TResult' response object when successful.
        // Throws HttpRequestException when non-success responses are retured from the server.
        public async Task<TResult> Get<TResult>(string requestUri) where TResult : class
        {
            var response = await _client.GetAsync(requestUri);

            // NOTE: This will throw HttpRequestException for non success responses from the server
            response.EnsureSuccessStatusCode();

            // Return the deserialized (TResult) response
            return await response.Content.ReadAsAsync<TResult>(new[] { jsonFormatter });

        }

        // ----------------------------------------------------------------------------------------
        // Send a POST request with 'TBody' to the server.
        // Returns a 'TResult' response object when successful.
        // Throws HttpRequestException when non-success responses are retured from the server.
        public async Task<TResult> Post<TResult, TBody>(string requestUri, TBody body)
        {

            var response = await _client.PostAsJsonAsync(requestUri, body);

            // NOTE: This will throw HttpRequestException for non success responses from the server
            response.EnsureSuccessStatusCode();

            // Return the deserialized (TResult) response
            return await response.Content.ReadAsAsync<TResult>(new[] { jsonFormatter });
        }
    }
}
