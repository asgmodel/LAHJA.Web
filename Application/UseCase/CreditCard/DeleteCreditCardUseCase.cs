using Domain.Repository.Billing;
using Domain.Repository.CreditCard;
using Domain.ShareData.Base;
using Domain.Wrapper;

namespace Application.Services.Plans
{
    public class DeleteCreditCardUseCase
    {
        private readonly ICreditCardRepository repository;

        public DeleteCreditCardUseCase(ICreditCardRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<DeleteResponse>> ExecuteAsync(string cardId)
        {
   
            return await repository.DeleteAsync(cardId);
        }
    }



}