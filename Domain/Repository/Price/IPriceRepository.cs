using Domain.Entities.Price.Request;
using Domain.Entities.Price.Response;
using Domain.ShareData.Base;
using Domain.Wrapper;


namespace Domain.Repository.Price
{
    public interface IPriceRepository
    {

        Task<Result<List<PriceResponse>>> SearchAsync(PriceSearchOptionsRequest request);
        Task<Result<PriceResponse>> CreateAsync(PriceCreate request);
        Task<Result<DeleteResponse>> DeleteAsync(string id);
        Task<Result<PriceResponse>> UpdateAsync(PriceUpdate request);
    }  
}