using Application.Services.Plans;
using AutoMapper;
using Domain.Wrapper;
using LAHJA.Helpers.Services;
using Domain.Entities.Checkout.Response;
using Domain.Entities.Checkout;
using Domain.Entities.Checkout.Request;
using Application.Services.Checkout;

namespace LAHJA.ApplicationLayer.Checkout
{
    public class CheckoutClientService
    {
        private readonly CheckoutService paymentService;
        private readonly TokenService tokenService;
        private readonly IMapper _mapper;



        public CheckoutClientService(CheckoutService paymentService, IMapper mapper, TokenService tokenService)
        {

            this.paymentService = paymentService;
            _mapper = mapper;
            this.tokenService = tokenService;
        }




        public async Task<Result<CheckoutResponse>> CheckoutAsync(CheckoutRequest request)
        {

            var result=await paymentService.CheckoutAsync(request);
            return result;

       

        }     
        
        public async Task<Result<CheckoutResponse>> CheckoutManageAsync(SessionCreate request)
        {

            var result=await paymentService.CheckoutManageAsync(request);
            return result;

       
        }

    }
}
