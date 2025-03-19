using Domain.Entities.Checkout.Response;
using Domain.Entities.Checkout;
using Domain.Wrapper;
using Domain.Entities.Checkout.Request;

namespace Domain.Repository.Checkout
{
    public interface ICheckoutRepository
    {
        Task<Result<CheckoutResponse>> CheckoutAsync(CheckoutRequest request);
        Task<Result<CheckoutResponse>> CheckoutManageAsync(SessionCreate request);
    }

}

