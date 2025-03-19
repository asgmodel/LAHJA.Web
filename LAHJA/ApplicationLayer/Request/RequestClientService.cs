using Application.Services.Service;
using AutoMapper;
using Domain.Entities.Event.Request;
using Domain.Entities.Event.Response;
using Domain.Entities.Request.Request;
using Domain.Entities.Request.Response;
using Domain.Entities.Service.Response;
using Domain.Wrapper;
using LAHJA.Helpers.Services;

namespace LAHJA.ApplicationLayer.Request
{
    public class RequestClientService
    {
        private readonly RequestService requestService;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;


        public RequestClientService(RequestService requestService, IMapper mapper,
            TokenService tokenService)
        {
            this.requestService = requestService;
            _mapper = mapper;
            _tokenService = tokenService;
       
        }



        public async Task<Result<RequestResponse>> CreateRequestAsync(RequestCreate request)
        {
      
            return await requestService.CreateRequestAsync(request);
        }

        public async Task<Result<RequestResponse>> RequestAllowedAsync(string serviceIds)
        {
            //var mappedRequest = _mapper.Map<ServiceRequest>(request);
            return await requestService.RequestAllowedAsync(serviceIds);
        }
        public async Task<Result<EventResponse>> CreateEventAsync(EventRequest request)
        {
            return await requestService.CreateEventAsync(request);
        }
        public async Task<Result<ServiceResponse>> ResultRequestAsync(ResultRequest request)
        {

            return await requestService.ResultRequestAsync(request);
        }
    }

}
