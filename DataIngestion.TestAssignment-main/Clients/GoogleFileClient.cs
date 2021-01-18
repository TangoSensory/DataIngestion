using DataIngestion.TestAssignment.Infrastructure;
using DataIngestion.TestAssignment.Interfaces;
using HelperTrinity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Clients
{
    public class GoogleFileClient : IGoogleFileClient
    {
        private HttpClient httpClient = null;

        public GoogleFileClient()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<byte[]> Retrieve(string fileId)
        {
            byte[] result = null;

            try
            {
                fileId.AssertNotNullOrWhiteSpace(nameof(fileId));

                string url = $"{GlobalConstants.GoogleDriveDirectDownloadUrlRoot}{fileId}";
                this.SetAuthorizationHeader(this.httpClient);

                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await this.httpClient.SendAsync(req);
                response.EnsureSuccessStatusCode();
                var cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
                var downloadConfirmationCookie = cookies.SingleOrDefault(x => x.Contains("download_warning")).Split(";").First(x => x.StartsWith("download_warning"));
                var downloadConfCode = downloadConfirmationCookie.Split("=").Last();
                var content = response.Content;

                url = $"{GlobalConstants.GoogleDriveDirectDownloadConfirmationUrlRoot}{downloadConfCode}{GlobalConstants.GoogleDriveDirectDownloadConfirmationUrlPart2}{fileId}";
                req = new HttpRequestMessage(HttpMethod.Get, url);
                req.Headers.Add("Set-Cookie", downloadConfirmationCookie);
                response = await this.httpClient.SendAsync(req);
                response.EnsureSuccessStatusCode();
                cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
                content = response.Content;

                result = await content.ReadAsByteArrayAsync();
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
