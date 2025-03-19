using Domain.Entities.Billing.Request;
using Domain.Entities.Billing.Response;
using Domain.Repository.Billing;
using Domain.Repository.CreditCard;
using Domain.Wrapper;

namespace Application.Services.Plans
{
    public class CreateCreditCardUseCase
    {
        private readonly ICreditCardRepository repository;

        public CreateCreditCardUseCase(ICreditCardRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<CardDetailsResponse>> ExecuteAsync(CardDetailsRequest request)
        {
        
            return await repository.CreateAsync(request);
        }
    }



}