using AutoMapper;
using Domain.Entities.Billing.Request;
using Domain.ShareData.Base;
using Domain.Wrapper;
using Infrastructure.DataSource.ApiClientFactory;
using Infrastructure.Models.Billing.Response;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataSource.ApiClient.Base
{



    public interface IBuildApiClient<T> {

        public string DevelopmentMessage { get; }


        public  Task<T> GetApiClient();
       
    }
    public class BuildApiClient<T>: IBuildApiClient<T> where T : class
    {

        public  string DevelopmentMessage => "The service is still under development From  Api !! ";

        protected readonly ClientFactory _clientFactory;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _config;
        public BuildApiClient(
                    ClientFactory clientFactory,
                    IMapper mapper,
                    IConfiguration config)
        {
            _clientFactory = clientFactory;
            _mapper = mapper;
            _config = config;
        }

     


        
        public  async Task<T>  GetApiClient()
        {
            var client = await _clientFactory.CreateClientWithAuthAsync<T>("ApiClient");
            return client;
        }

      
    }
}
