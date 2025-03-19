using Domain.Entities.Billing.Response;
using Domain.Repository.Billing;
using Domain.Wrapper;

namespace Application.Services.Plans
{
    public class GetBillingDetailsUseCase
    {

        private readonly IBillingRepository repository;
        public GetBillingDetailsUseCase(IBillingRepository repository)
        {

            this.repository = repository;
        }


        public async Task<Result<BillingDetailsResponse>> ExecuteAsync()
        {

            return await repository.GetBillingDetailsAsync();

        }


    }


}
