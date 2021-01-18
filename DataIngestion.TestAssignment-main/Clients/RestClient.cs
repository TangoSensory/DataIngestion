using DataIngestion.TestAssignment.Interfaces;
using HelperTrinity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Clients
{
    public class RestClient : IRestClient
    {
        private HttpClient httpClient = null;
        private JsonMediaTypeFormatter defaultJsonFormatter = null;
        private JsonSerializerSettings defaultJsonSerializerSettings = null;

        public RestClient()
        {
            this.httpClient = new HttpClient();
            this.defaultJsonFormatter = new JsonMediaTypeFormatter();
            this.defaultJsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            this.defaultJsonFormatter.SerializerSettings = this.defaultJsonSerializerSettings;
        }

        public async Task<T> Create<T>(string path, T item, JsonSerializerSettings jsonSerializerSettings = null)
        {
            var typeFormatter = jsonSerializerSettings == null ? this.defaultJsonFormatter : new JsonMediaTypeFormatter { SerializerSettings = jsonSerializerSettings };
            T result = default;

            try
            {
                path.AssertNotNullOrWhiteSpace(nameof(path));
                item.AssertGenericArgumentNotNull(nameof(item));

                this.SetAuthorizationHeader(this.httpClient);
                var response = await this.httpClient.PostAsync(path, item, typeFormatter).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                    {
                        // log error
                        throw x.Exception;
                    }

                    result = JsonConvert.DeserializeObject<T>(x.Result, typeFormatter.SerializerSettings);

                });
            }
            catch (Exception ex)
            {
                // log error
                throw ex;
            }

            return result;
        }

        private void SetAuthorizationHeader(HttpClient httpClient)
        {
            // If required
        }
    }
}
