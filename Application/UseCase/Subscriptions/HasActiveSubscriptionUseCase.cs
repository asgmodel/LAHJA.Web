using Domain.Repository.Subscriptions;
using Domain.Wrapper;

namespace Application.UseCase.Plans.Get
{
    public class HasActiveSubscriptionUseCase
    {
        private readonly ISubscriptionsRepository repository;
        public HasActiveSubscriptionUseCase(ISubscriptionsRepository repository)
        {

            this.repository = repository;
        }

        public async Task<Result<bool>> ExecuteAsync()
        {

            return await repository.HasActiveSubscriptionAsync();

        }
    }  






}
