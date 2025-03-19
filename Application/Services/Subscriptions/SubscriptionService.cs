using Application.UseCase.Plans.Get;
using Domain.Entities.Subscriptions.Request;
using Domain.Entities.Subscriptions.Response;
using Domain.ShareData.Base;
using Domain.Wrapper;


namespace Application.Services.Subscriptions
{
    public class SubscriptionService
    {
        private readonly PauseSubscriptionUseCase pauseSubscriptionUseCase;
        private readonly DeleteSubscriptionUseCase deleteSubscriptionUseCase;
        private readonly ResumeSubscriptionUseCase resumeSubscriptionUseCase;
        private readonly GetAllSubscriptionsUseCase getAllSubscriptionsUseCase;
        private readonly CreateSubscriptionUseCase createSubscriptionUseCase;
        private readonly UpdateSubscriptionUseCase updateSubscriptionUseCase;
        private readonly HasActiveSubscriptionUseCase hasActiveSubscriptionUseCase;

        public SubscriptionService(
            PauseSubscriptionUseCase pauseSubscriptionUseCase,
            DeleteSubscriptionUseCase deleteSubscriptionUseCase,
            ResumeSubscriptionUseCase resumeSubscriptionUseCase,
            GetAllSubscriptionsUseCase getAllSubscriptionsUseCase,
            CreateSubscriptionUseCase createSubscriptionUseCase,
            UpdateSubscriptionUseCase updateSubscriptionUseCase,
            HasActiveSubscriptionUseCase hasActiveSubscriptionUseCase)
        {
            this.pauseSubscriptionUseCase = pauseSubscriptionUseCase;
            this.deleteSubscriptionUseCase = deleteSubscriptionUseCase;
            this.resumeSubscriptionUseCase = resumeSubscriptionUseCase;
            this.getAllSubscriptionsUseCase = getAllSubscriptionsUseCase;
            this.createSubscriptionUseCase = createSubscriptionUseCase;
            this.updateSubscriptionUseCase = updateSubscriptionUseCase;
            this.hasActiveSubscriptionUseCase = hasActiveSubscriptionUseCase;
        }
        public async Task<Result<bool>> HasActiveSubscriptionAsync()
        {
            return await hasActiveSubscriptionUseCase.ExecuteAsync();
        }
        public async Task<Result<SubscriptionCreateResponse>> CreateAsync(SubscriptionCreate request)
        {
            return await createSubscriptionUseCase.ExecuteAsync(request);
        }
        public async Task<Result<SubscriptionResponse>> UpdateAsync(SubscriptionRequest request)
        {
            return await updateSubscriptionUseCase.ExecuteAsync(request);
        }
        public async Task<Result<SubscriptionResponse>> PauseAsync(string id)
        {
            return await pauseSubscriptionUseCase.ExecuteAsync(id);
        }

        public async Task<Result<DeleteResponse>> DeleteAsync(string id)
        {
            return await deleteSubscriptionUseCase.ExecuteAsync(id);
        }

        public async Task<Result<SubscriptionResponse>> ResumeAsync(string id)
        {
            return await resumeSubscriptionUseCase.ExecuteAsync(id);
        }

        public async Task<Result<List<SubscriptionResponse>>> GetAllAsync()
        {
            return await getAllSubscriptionsUseCase.ExecuteAsync();
        }
    }

}
