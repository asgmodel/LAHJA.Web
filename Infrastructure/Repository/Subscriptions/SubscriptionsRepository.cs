
using AutoMapper;
using Domain.Wrapper;
using Infrastructure.DataSource.Seeds;

using Shared.Settings;
using Infrastructure.DataSource.ApiClient.Payment;

using Domain.ShareData.Base;
using Domain.Repository.Subscriptions;
using Infrastructure.Models.Plans;
using Domain.Entities.Subscriptions.Response;
using Infrastructure.Models.Subscriptions.Request;
using Infrastructure.Models.Subscriptions.Response;
using Domain.Entities.Subscriptions.Request;
using Infrastructure.DataSource.Seeds.Models;
using Domain.ShareData;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Infrastructure.Repository.Subscription
{

    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        private readonly SeedsSubscriptionsPlans seedsPlans;
        private readonly SeedsSubscriptionsData seedsSubscriptionsData;
        private readonly SubscriptionsApiClient subscriptionApiClient;
        private readonly IMapper _mapper;
        private readonly ISessionUserManager _sessionUserManager;
        private readonly ApplicationModeService appModeService;
        public SubscriptionsRepository(
            IMapper mapper,
            SeedsSubscriptionsPlans seedsPlans,
            ApplicationModeService appModeService,
            SubscriptionsApiClient subscriptionApiClient,
            SeedsSubscriptionsData seedsSubscriptionsData,
            ISessionUserManager sessionUserManager)
        {

            //seedsPlans = new SeedsPlans();
            _mapper = mapper;
            this.seedsPlans = seedsPlans;
            this.appModeService = appModeService;

            this.subscriptionApiClient = subscriptionApiClient;
            this.seedsSubscriptionsData = seedsSubscriptionsData;
            _sessionUserManager = sessionUserManager;
        }

        public async Task<Result<bool>> HasActiveSubscriptionAsync()
        {
            var response = await ExecutorAppMode.ExecuteAsync<Result<List<SubscriptionResponseModel>>>(
                 async () => await subscriptionApiClient.getAllAsync(),
                  async () => {
                      var email = await _sessionUserManager.GetEmailAsync();
                      if (email == null)
                          return Result<List<SubscriptionResponseModel>>.Fail();

                      var data = seedsSubscriptionsData.getActiveSubscriptions(email);
                      if(data != null)
                      {
                          var items=_mapper.Map<List<SubscriptionResponseModel>>(data);
                          return Result<List<SubscriptionResponseModel>>.Success(items);
                      }
                  
                    
                      return Result<List<SubscriptionResponseModel>>.Fail();
                  });

            if (response.Succeeded)
            {
                return Result<bool>.Success(true);
            }
            else
            {
                return Result<bool>.Fail();
            }


        }
        public async Task<Result<List<SubscriptionResponse>>> getAllAsync()
        {
            var response = await ExecutorAppMode.ExecuteAsync<Result<List<SubscriptionResponseModel>>>(
                 async () => await subscriptionApiClient.getAllAsync(),
                  async () => {
                      var email = await _sessionUserManager.GetEmailAsync();
                      if (email == null)
                          return Result<List<SubscriptionResponseModel>>.Fail();

                      var data = seedsSubscriptionsData.getAllUserSubscriptions(email);
                      if (data != null)
                      {
                          var items = _mapper.Map<List<SubscriptionResponseModel>>(data);
                          return Result<List<SubscriptionResponseModel>>.Success(items);
                      }


                      return Result<List<SubscriptionResponseModel>>.Fail();
                  });

            if (response.Succeeded)
            {
                var result = (response.Data != null) ? _mapper.Map<List<SubscriptionResponse>>(response.Data) : null;
                return Result<List<SubscriptionResponse>>.Success(result);
            }
            else
            {
                return Result<List<SubscriptionResponse>>.Fail(response.Messages);
            }


        }

        public async Task<Result<SubscriptionResponse>> PauseAsync(string id)
        {
            var response = await ExecutorAppMode.ExecuteAsync<Result<SubscriptionResponseModel>>(
                 async () => await subscriptionApiClient.PauseAsync(id),
                  async () => Result<SubscriptionResponseModel>.Success());

            if (response.Succeeded)
            {
                var result = (response.Data != null) ? _mapper.Map<SubscriptionResponse>(response.Data) : null;
                return Result<SubscriptionResponse>.Success(result);
            }
            else
            {
                return Result<SubscriptionResponse>.Fail(response.Messages);
            }



        }

        public async Task<SubscriptionCreateResponse> CreateAsync(SubscriptionCreate request)
        {
            var smodel=_mapper.Map<SubscriptionCreateModel>(request);
            var response = await ExecutorAppMode.ExecuteAsync(
                  async () => await subscriptionApiClient.CreateSubscriptionAsync(smodel),
                  async () =>
                  {
                      return await subscriptionApiClient.CreateSubscriptionAsync(smodel);
                      //var email = await _sessionUserManager.GetEmailAsync();
                      //if(email == null)
                      //    return Result<SubscriptionResponseModel>.Fail();

                      //var plan= seedsPlans.getOne(request.PlanId);
                      //if (plan == null)
                      //    return Result<SubscriptionResponseModel>.Fail();

                      //var model=_mapper.Map<SubscriptionModel>(request);
                      //model.UserId = email;
                      //model.PlanId = plan.Id;
                      //model.SubscriptionPlan = plan;
                      //model.SubscriptionPlan.Active = true; 
                      //model.StartDate = DateTime.Now;
                      //model.CancelAtPeriodEnd = true;

                      //seedsSubscriptionsData.AddSubscription(model);
                      //var res = seedsSubscriptionsData.SearchSubscriptions(x => x.UserId == email).FirstOrDefault();
                      //if (res != null)
                      //{
                      //    var data = _mapper.Map<SubscriptionResponseModel>(res);
                      //    return Result<SubscriptionResponseModel>.Success(data);
                      //}
                      //return Result<SubscriptionResponseModel>.Fail();
                  });

            return _mapper.Map<SubscriptionCreateResponse>(response);

            //if (response.Succeeded)
            //{
            //    var result = (response.Data != null) ? _mapper.Map<SubscriptionCreateResponse>(response) : null;
            //    return Result<SubscriptionResponse>.Success(result);
            //}
            //else
            //{
            //    return Result<SubscriptionResponse>.Fail(response.Messages);
            //}
        }
        public async Task<Result<SubscriptionResponse>> ResumeAsync(string id)
        {
            var response = await ExecutorAppMode.ExecuteAsync<Result<SubscriptionResponseModel>>(
                 async () => await subscriptionApiClient.ResumeAsync(id),
                  async () => Result<SubscriptionResponseModel>.Success());

            if (response.Succeeded)
            {
                var result = (response.Data != null) ? _mapper.Map<SubscriptionResponse>(response.Data) : null;
                return Result<SubscriptionResponse>.Success(result);
            }
            else
            {
                return Result<SubscriptionResponse>.Fail(response.Messages);
            }

        }

        public async Task<Result<DeleteResponse>> DeleteAsync(string id)
        {
            var response = await ExecutorAppMode.ExecuteAsync<Result<SubscriptionResponseModel>>(
                 async () => await subscriptionApiClient.DeleteAsync(id),
                  async () => Result<SubscriptionResponseModel>.Success());

            if (response.Succeeded)
            {
                //var result = (response.Data != null) ? _mapper.Map<DeleteResponse>(response.Data) : null;
                return Result<DeleteResponse>.Success();
            }
            else
            {
                return Result<DeleteResponse>.Fail(response.Messages);
            }
        }

        public async Task<Result<SubscriptionResponse>> UpdateAsync(SubscriptionRequest request)
        {
            var response = await ExecutorAppMode.ExecuteAsync<Result<SubscriptionResponseModel>>(
                   async () => Result<SubscriptionResponseModel>.Success(),
                   async () =>
                   {
                       var email = await _sessionUserManager.GetEmailAsync();
                       if (email == null)
                           return Result<SubscriptionResponseModel>.Fail();

                       var plan = seedsPlans.getOne(request.PlanId);
                       if (plan == null)
                           return Result<SubscriptionResponseModel>.Fail();

                       var model = _mapper.Map<SubscriptionModel>(request);
                       model.UserId = email;
                       model.PlanId = plan.Id;
                       model.SubscriptionPlan = plan;
                       model.StartDate = DateTime.Now;
                       model.CancelAtPeriodEnd = true;

                       if (seedsSubscriptionsData.UpdateSubscription(model.Id, model))
                       {
                           var res= seedsSubscriptionsData.SearchSubscriptions(x=>x.UserId==email).FirstOrDefault();
                           if (res != null)
                           {
                               var data=_mapper.Map<SubscriptionResponseModel>(res);
                               return Result<SubscriptionResponseModel>.Success(data);
                           }
                              
                       }
                            
                       
                       return Result<SubscriptionResponseModel>.Fail();
                   });

            if (response.Succeeded)
            {
                var result = (response.Data != null) ? _mapper.Map<SubscriptionResponse>(response.Data) : null;
                return Result<SubscriptionResponse>.Success(result);
            }
            else
            {
                return Result<SubscriptionResponse>.Fail(response.Messages);
            }
        }
    } 
}
