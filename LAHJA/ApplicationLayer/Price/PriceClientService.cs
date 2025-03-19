using Application.Services.Plans;
using AutoMapper;
using Domain.Wrapper;
using LAHJA.Helpers.Services;
using Domain.Entities.Price.Response;
using Domain.Entities.Price;
using Domain.Entities.Price.Request;
using Domain.ShareData.Base;

namespace LAHJA.ApplicationLayer.Price
{
    public class PriceClientService
    {
        private readonly PriceService _priceService;
        private readonly TokenService tokenService;
        private readonly IMapper _mapper;



        public PriceClientService(PriceService PriceService, IMapper mapper, TokenService tokenService)
        {

            this._priceService = PriceService;
            _mapper = mapper;
            this.tokenService = tokenService;
        }




        public async Task<Result<List<PriceResponse>>> SearchAsync(PriceSearchOptionsRequest request)
        {
            return await _priceService.SearchAsync(request);
        }
        public async Task<Result<PriceResponse>> CreateAsync(PriceCreate request)
        {
            return await _priceService.createAsync(request);
        }
        public async Task<Result<DeleteResponse>> DeleteAsync(string id)
        {
            return await _priceService.DeleteAsync(id);
        }
        public async Task<Result<PriceResponse>> UpdateAsync(PriceUpdate request)
        {
            return await _priceService.UpdateAsync(request);
        }

    }
}
