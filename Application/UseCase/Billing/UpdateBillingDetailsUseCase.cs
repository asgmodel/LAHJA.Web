﻿using Domain.Entities.Billing.Request;
using Domain.Entities.Billing.Response;
using Domain.Repository.Billing;
using Domain.Wrapper;

namespace Application.Services.Plans
{
    public class UpdateBillingDetailsUseCase
    {
        private readonly IBillingRepository repository;

        public UpdateBillingDetailsUseCase(IBillingRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<BillingDetailsResponse>> ExecuteAsync(BillingDetailsRequest request)
        {
     

            return await repository.UpdateBillingAsync(request);
        }
    }


}
