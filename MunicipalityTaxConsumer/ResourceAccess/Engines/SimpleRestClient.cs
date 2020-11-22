using BusinessLogic.Contract;
using IdentityModel.Client;
using ResourceAccess.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ResourceAccess.Engines
{
    public class SimpleRestClient : ISimpleRestClient
    {
        private HttpClient client;

        public SimpleRestClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IRestServiceResponse> InvokeServiceAsync(IRestServiceRequest request)
        {
            try
            {
                // TODO Create a specific IRestSecureServiceRequest
                if (!string.IsNullOrEmpty(request.ClientId))
                {
                    var identityClient = new HttpClient();

                    var disco = await identityClient.GetDiscoveryDocumentAsync("https://localhost:5001"); // TODO get identity endpoint from config
                    if (disco.IsError)
                        throw new ApplicationException(disco.Error);

                    var tokenResponse = await identityClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                    {
                        Address = disco.TokenEndpoint,

                        ClientId = request.ClientId,
                        ClientSecret = request.ClientSecret,
                        Scope = request.Scope
                    });

                    if (tokenResponse.IsError)
                        throw new ApplicationException(tokenResponse.Error);

                    client.SetBearerToken(tokenResponse.AccessToken);
                }

                var baseUri = new Uri($"{request.Host}");
                client.BaseAddress = baseUri;


                HttpRequestMessage httpRequest;
                switch (request.Method.ToLower())
                {
                    case "get":
                        httpRequest = new HttpRequestMessage(HttpMethod.Get, request.Path);
                        break;
                    case "put":
                        httpRequest = new HttpRequestMessage(HttpMethod.Put, request.Path);
                        httpRequest.Content = new StringContent(request.Body, Encoding.UTF8, "application/json");
                        break;
                    default:
                        throw new ApplicationException("Unsupported method!");
                }

                var response = await client.SendAsync(httpRequest);

                var serviceResponse = new RestServiceResponse
                {
                    Success = response.StatusCode == HttpStatusCode.OK,
                    StatusCode = response.StatusCode,
                    Content = await response.Content.ReadAsStringAsync()
                };

                return serviceResponse;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
