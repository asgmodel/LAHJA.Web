using Application.Services.Plans;
using AutoMapper;
using Domain.Wrapper;
using LAHJA.Helpers.Services;
using Domain.Entities.Product.Response;
using Domain.Entities.Product;
using Domain.Entities.Product.Request;
using Application.UseCase.Plans.Get;
using Domain.Entities.Price.Request;
using Domain.ShareData.Base;
using Domain.Entities.Price.Response;
using Application.Services.Prroduct;

namespace LAHJA.ApplicationLayer.Product
{
    public class ProductClientService
    {
        private readonly ProductService _productService;
        private readonly TokenService tokenService;
        private readonly IMapper _mapper;



        public ProductClientService(ProductService ProductService, IMapper mapper, TokenService tokenService)
        {

            this._productService = ProductService;
            _mapper = mapper;
            this.tokenService = tokenService;
        }



        public async Task<Result<List<ProductResponse>>> SearchAsync(ProductSearchRequest request)
        {
            return await _productService.SearchAsync(request);
        }
        public async Task<Result<List<ProductResponse>>> GetAllAsync(FilterResponseData filter)
        {
            return await _productService.GetAllAsync(filter);
        }
        public async Task<Result<ProductResponse>> CreateAsync(ProductCreate request)
        {
            return await _productService.CreateAsync(request);
        }
        public async Task<Result<DeleteResponse>> DeleteAsync(string id)
        {
            return await _productService.DeleteAsync(id);
        }
        public async Task<Result<ProductResponse>> UpdateAsync(ProductUpdate request)
        {
            return await _productService.UpdateAsync(request);
        }

    }
}
