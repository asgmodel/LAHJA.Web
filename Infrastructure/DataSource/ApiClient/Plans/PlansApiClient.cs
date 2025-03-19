using AutoMapper;
using Domain.Wrapper;
using Infrastructure.DataSource.ApiClientFactory;
using Infrastructure.Models.BaseFolder.Response;
using Infrastructure.Models.Plans;
using Infrastructure.Models.Plans.Response;
using Infrastructure.Nswag;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataSource.ApiClient.Plans
{
    using Domain.ShareData.Base;
    using Infrastructure.DataSource.ApiClient.Base;
    using System.Collections.Generic;

    public class PlansApiClient:BuildApiClient<PlansClient>
    {
        public PlansApiClient(ClientFactory clientFactory, IMapper mapper, IConfiguration config) : base(clientFactory, mapper, config)
        {
        }


        //public async Task<Result<IEnumerable<SubscriptionPlanModel>>> getAllPlansAsync(FilterResponseData filter)
        //{
        //    try
        //    {

        //        var client = await GetApiClient();
        //        var response = await client.GetPlansAsync(filter.lg); 


        //        var resModel = _mapper.Map<IEnumerable<SubscriptionPlanModel>>(response);
        //        return Result<IEnumerable<SubscriptionPlanModel>>.Success();

        //    }
        //    catch (ApiException e)
        //    {

        //        return Result<IEnumerable<SubscriptionPlanModel>>.Fail(e.Response, httpCode: e.StatusCode);

        //    }



        //}

        public async Task<Result<SubscriptionPlanModel>> CreatePlanAsync(PlanCreateModel request)
        {
            try
            {
                var model = _mapper.Map<PlanCreate>(request);
                var client = await GetApiClient();
                //var response = null; //await client.CreatePlanAsync(model);


                //var resModel = null;// _mapper.Map<SubscriptionPlanModel>(response);
                return Result<SubscriptionPlanModel>.Success();

            }
            catch (ApiException e)
            {

                return Result<SubscriptionPlanModel>.Fail(e.Response, httpCode: e.StatusCode);

            }



        }    
        
        public async Task<Result<SubscriptionPlanModel>> UpdatePlanAsync(PlanUpdateModel request)
        {
            try
            {
                var model = _mapper.Map<PlanUpdate>(request);
                var client = await GetApiClient();
                var response = await client.UpdatePlanAsync(request.Id,model);


                var resModel = _mapper.Map<SubscriptionPlanModel>(response);
                return Result<SubscriptionPlanModel>.Success();

            }
            catch (ApiException e)
            {

                return Result<SubscriptionPlanModel>.Fail(e.Response, httpCode: e.StatusCode);

            }



        } 
        public async Task<Result<DeleteResponseModel>> DeletePlanAsync(string id)
        {
            try
            {
              
                var client = await GetApiClient();
                var response = await client.DeletePlanAsync(id);


                var resModel = _mapper.Map<DeleteResponseModel>(response);
                return Result<DeleteResponseModel>.Success();

            }
            catch (ApiException e)
            {

                return Result<DeleteResponseModel>.Fail(e.Response, httpCode: e.StatusCode);

            }



        }
        //public async Task<Result<IEnumerable<PlansGroupModel>>> getPlansGroupAsync(FilterResponseData filter)
        //{
        //    try
        //    {

        //        var client = await GetApiClient();
        //        var response =  await client.AsGroupAsync(filter.lg);


        //        var resModel = _mapper.Map<IEnumerable<PlansGroupModel>>(response);
        //        return Result<IEnumerable<PlansGroupModel>>.Success();

        //    }
        //    catch (ApiException e)
        //    {

        //        return Result<IEnumerable<PlansGroupModel>>.Fail(e.Response, httpCode: e.StatusCode);

        //    }



        //}
        public async Task<Result<IEnumerable<SubscriptionPlanModel>>> GetPlansAsync(FilterResponseData filter)
        {
            try
            {

                var client = await GetApiClient();
                var response = await client.AsGroupAsync(filter.lg);
                var response2 = response.OrderBy(p => p.Amount).ToList();
                

                if (response == null)
                    return Result<IEnumerable<SubscriptionPlanModel>>.Success();


                var resModel = _mapper.Map<IEnumerable<SubscriptionPlanModel>>(response2);
                return Result<IEnumerable<SubscriptionPlanModel>>.Success(resModel);

            }
            catch (ApiException e)
            {

                return Result<IEnumerable<SubscriptionPlanModel>>.Fail(e.Response,httpCode:e.StatusCode);

            }



        }
        public async Task<Result<SubscriptionPlanModel>> GetPlanAsync(string id,string lg="en")
        {
            try
            {

                var client = await GetApiClient();
                var response = await client.GetPlanAsync(id,lg);
                var resModel = _mapper.Map<SubscriptionPlanModel>(response);
                return Result<SubscriptionPlanModel>.Success(resModel);

            }
            catch (ApiException e)
            {

                return Result<SubscriptionPlanModel>.Fail(e.Response, httpCode: e.StatusCode);

            }



        }
    }
}
