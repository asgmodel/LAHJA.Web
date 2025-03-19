using IdentityModel.Client;
using Shared.Helpers;
using System.Net.Http.Headers;

namespace Infrastructure.DataSource.ApiClientFactory
{

    


    public class ClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly BaseUrl baseUrl;
        private readonly ITokenService tokenService;
        //private readonly IUserClaimsHelper userClaimsHelper;

        public ClientFactory(IHttpClientFactory httpClientFactory,
            BaseUrl baseUrl, IUserClaimsHelper userClaimsHelper, ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory), "HttpClientFactory cannot be null.");
            this.baseUrl = baseUrl;
            //this.userClaimsHelper = userClaimsHelper;
            this.tokenService = tokenService;
        }

        public async Task<TClient> CreateClientAsync<TClient>(string clientName="ApiClient") where TClient : class
        {

          


            if (string.IsNullOrWhiteSpace(clientName))
            {
                throw new ArgumentException("Client name cannot be null, empty or whitespace.", nameof(clientName));
            }

            try
            {

                var httpClient = _httpClientFactory.CreateClient(clientName);
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userClaimsHelper.AccessToken);


                if (Activator.CreateInstance(typeof(TClient), httpClient) is TClient client)
                {
                    return client;
                }
                else
                {
                    throw new InvalidOperationException($"Could not create an instance of {typeof(TClient).Name}. Make sure the constructor is correct.");
                }
            }
            catch (ArgumentException ex)
            {

                throw new InvalidOperationException("Invalid argument provided to create the client.", ex);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"An error occurred while creating the client: {ex.Message}", ex);
            }
        }
        public async Task<TClient> CreateClientWithAuthAsync<TClient>(string clientName = "ApiClient",string token2="") where TClient : class
        {


         

            if (string.IsNullOrWhiteSpace(clientName))
            {
                throw new ArgumentException("Client name cannot be null, empty or whitespace.", nameof(clientName));
            }

            try
            {
                var token = "";

                if (string.IsNullOrEmpty(token2))
                    token = await tokenService.GetTokenAsync();
                else
                    token = token2;

				if (token == null)
                    throw new Exception("invalid token!!");

                var httpClient = _httpClientFactory.CreateClient(clientName);
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
      
                 if (!string.IsNullOrEmpty(token)) httpClient.SetBearerToken(token);

                if (Activator.CreateInstance(typeof(TClient),httpClient) is TClient client)
                {
                    return client;
                }
                else
                {
                    throw new InvalidOperationException($"Could not create an instance of {typeof(TClient).Name}. Make sure the constructor is correct.");
                }
            }
            catch (ArgumentException ex)
            {

                throw new InvalidOperationException("Invalid argument provided to create the client.", ex);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"An error occurred while creating the client: {ex.Message}", ex);
            }
        }
    }
}