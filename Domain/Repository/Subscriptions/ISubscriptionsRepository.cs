using Domain.Entities.Product.Request;
using Domain.Entities.Product.Response;
using Domain.Entities.Subscriptions.Request;
using Domain.Entities.Subscriptions.Response;
using Domain.ShareData.Base;
using Domain.Wrapper;


namespace Domain.Repository.Subscriptions
{
    public interface ISubscriptionsRepository
    {
        Task<Result<bool>> HasActiveSubscriptionAsync();
        Task<Result<DeleteResponse>> DeleteAsync(string id);
        Task<Result<SubscriptionResponse>> ResumeAsync(string id);
        Task<Result<SubscriptionResponse>> PauseAsync(string id);
        Task<Result<List<SubscriptionResponse>>> getAllAsync();
        //Task<Result<SubscriptionResponse>> CreateAsync(SubscriptionRequest request);
        Task<SubscriptionCreateResponse> CreateAsync(SubscriptionCreate request);
        Task<Result<SubscriptionResponse>> UpdateAsync(SubscriptionRequest request);
    }
}