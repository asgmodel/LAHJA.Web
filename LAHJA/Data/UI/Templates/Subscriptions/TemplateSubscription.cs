using AutoMapper;
using Domain.Entities.Subscriptions.Request;
using Domain.Entities.Subscriptions.Response;
using Domain.ShareData.Base;
using Domain.Wrapper;
using LAHJA.ApplicationLayer.Subscription;
using LAHJA.ApplicationLayer.Subscription;
using LAHJA.Data.UI.Components;
using LAHJA.Data.UI.Components.Subscription;
using LAHJA.Data.UI.Templates.Base;
using LAHJA.Data.UI.Templates.Subscription;
using LAHJA.Helpers.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;


namespace LAHJA.Data.UI.Templates.Subscription
{

    //public class DataBuildUserSubscriptionInfo
    //{
   
    //    public string? Id { get; set; }
    //    public string? UserId { get; set; }
    //    public string? PlanId { get; set; }
    //    public string? CustomerId { get; set; }
    //    public string? BillingPeriod { get; set; }
    //    public DateTimeOffset? StartDate { get; set; }
    //    public string? Status { get; set; }
    //    public bool? CancelAtPeriodEnd { get; set; }
    //}




    public interface IBuilderSubscriptionComponent<T> : IBuilderComponents<T>
    {
        public Func<T, Task> SubmitSearch { get; set; }
        public Func<Task> SubmitGetAll { get; set; }
        public Func<T, Task> SubmitPause { get; set; }
        public Func<T, Task> SubmitResume { get; set; }
        public Func<T, Task> SubmitDelete { get; set; }
        public Func<T, Task<Result<SubscriptionCreateResponse>>> SubmitCreate { get; set; }
        public Func<T, Task> SubmitUpdate { get; set; }


    }

    public interface IBuilderSubscriptionApi<T> : IBuilderApi<T>
    {


        //Task<Result<List<SubscriptionResponse>>> SearchAsync(T data);
        Task<Result<List<UserSubscription>>> GetAllAsync();
        Task<Result<bool>> HasActiveSubscriptionAsync();
        Task<Result<SubscriptionCreateResponse>> CreateAsync(T data);
        Task<Result<UserSubscription>> ResumeAsync(T data);
        Task<Result<UserSubscription>> PauseAsync(T data);
        Task<Result<DeleteResponse>> DeleteAsync(T data);
        Task<Result<UserSubscription>> UpdateAsync(T data);


    }

    public abstract class BuilderSubscriptionApi<T, E> : BuilderApi<T, E>, IBuilderSubscriptionApi<E>
    {

        public  BuilderSubscriptionApi(IMapper mapper, T service) : base(mapper, service)
        {

        }

        //public abstract Task<Result<SubscriptionResponse>> CreateAsync(E data);

    

        public abstract Task<Result<List<UserSubscription>>> GetAllAsync();
        public abstract Task<Result<bool>> HasActiveSubscriptionAsync();

        public abstract Task<Result<SubscriptionCreateResponse>> CreateAsync(E data);
        public abstract Task<Result<UserSubscription>> PauseAsync(E data);
        public abstract Task<Result<UserSubscription>> ResumeAsync(E data);
        public abstract Task<Result<DeleteResponse>> DeleteAsync(E dataId);

        public abstract Task<Result<UserSubscription>> UpdateAsync(E data);


    }
    public class BuilderSubscriptionComponent<T> : IBuilderSubscriptionComponent<T>
    {


        public Func<T, Task> SubmitSearch { get; set; }
        public Func <Task> SubmitGetAll { get; set; }
        public Func<T, Task> SubmitPause { get; set; }
        public Func<T, Task> SubmitResume { get; set; }
        public Func<T, Task> SubmitDelete { get; set; }
        public Func<T, Task> SubmitUpdate { get; set; }
        public Func<T, Task<Result<SubscriptionCreateResponse>>> SubmitCreate{ get; set; }


    }


    public class TemplateSubscriptionShare<T, E> : TemplateBase<T, E>
    {
        protected readonly NavigationManager navigation;
        protected readonly IDialogService dialogService;
        protected readonly ISnackbar Snackbar;
        protected IBuilderSubscriptionApi<E> builderApi;
        private readonly IBuilderSubscriptionComponent<E> builderComponents;
        public IBuilderSubscriptionComponent<E> BuilderComponents { get => builderComponents; }
        public TemplateSubscriptionShare(

               IMapper mapper,
               AuthService AuthService,
                T client,
                IBuilderSubscriptionComponent<E> builderComponents,
                NavigationManager navigation,
                IDialogService dialogService,
                ISnackbar snackbar


            ) : base(mapper, AuthService, client)
        {



            builderComponents = new BuilderSubscriptionComponent<E>();
            this.navigation = navigation;
            this.dialogService = dialogService;
            this.Snackbar = snackbar;
            //this.builderApi = builderApi;
            this.builderComponents = builderComponents;


        }

    }


    public class BuilderSubscriptionApiClient : BuilderSubscriptionApi<SubscriptionClientService, DataBuildUserSubscriptionInfo>, IBuilderSubscriptionApi<DataBuildUserSubscriptionInfo>
    {
        public BuilderSubscriptionApiClient(IMapper mapper, SubscriptionClientService service) : base(mapper, service)
        {

        }

        public override async Task<Result<UserSubscription>> PauseAsync(DataBuildUserSubscriptionInfo data)
        {
            //var model = Mapper.Map<SubscriptionCreate>(data);
            var res = await Service.PauseAsync(data.Id);
            if (res.Succeeded)
            {
                try
                {
                    var map = Mapper.Map<UserSubscription>(res.Data);
                    return Result<UserSubscription>.Success(map);

                }
                catch (Exception e)
                {
                    return Result<UserSubscription>.Fail();
                }
            }
            else
            {
                return Result<UserSubscription>.Fail(res.Messages);
            }
        }

        public override async Task<Result<DeleteResponse>> DeleteAsync(DataBuildUserSubscriptionInfo data)
        {

            var res = await Service.DeleteAsync(data.Id);
            if (res.Succeeded)
            {
                try
                {
                    return Result<DeleteResponse>.Success(res.Data);

                }
                catch (Exception e)
                {
                    return Result<DeleteResponse>.Fail();
                }
            }
            else
            {
                return Result<DeleteResponse>.Fail(res.Messages);
            }
        }

        public override async Task<Result<List<UserSubscription>>> GetAllAsync()
        {
            //var model = Mapper.Map<FilterResponseData>(filter);
            var res = await Service.GetAllAsync();
            if (res.Succeeded)
            {
                try
                {
                    var map = Mapper.Map<List<UserSubscription>>(res.Data);
                    return Result<List<UserSubscription>>.Success(map);

                }
                catch (Exception e)
                {
                    return Result<List<UserSubscription>>.Fail();
                }
            }
            else
            {
                return Result<List<UserSubscription>>.Fail(res.Messages);
            }

        }

        public override async Task<Result<UserSubscription>> ResumeAsync(DataBuildUserSubscriptionInfo data)
        {
            //var model = Mapper.Map<SubscriptionSearchRequest>(data);
            var res = await Service.ResumeAsync(data.Id);
            if (res.Succeeded)
            {
                try
                {
                    var map = Mapper.Map<UserSubscription>(res.Data);
                    return Result<UserSubscription>.Success(map);

                }
                catch (Exception e)
                {
                    return Result<UserSubscription>.Fail();
                }
            }
            else
            {
                return Result<UserSubscription>.Fail(res.Messages);
            }
        }

        public override async Task<Result<SubscriptionCreateResponse>> CreateAsync(DataBuildUserSubscriptionInfo data)
        {
            var model = Mapper.Map<SubscriptionCreate>(data);
            return await Service.CreateAsync(model);
            //if (res.Succeeded)
            //{
            //    try
            //    {
            //        var map = Mapper.Map<UserSubscription>(res.Data);
            //        return Result<UserSubscription>.Success(map);

            //    }
            //    catch (Exception e)
            //    {
            //        return Result<UserSubscription>.Fail();
            //    }
            //}
            //else
            //{
            //    return Result<UserSubscription>.Fail(res.Messages);
            //}
        }


        public override async Task<Result<UserSubscription>> UpdateAsync(DataBuildUserSubscriptionInfo data)
        {
            var model = Mapper.Map<SubscriptionRequest>(data);
            var res = await Service.UpdateAsync(model);
            if (res.Succeeded)
            {
                try
                {
                    var map = Mapper.Map<UserSubscription>(res.Data);
                    return Result<UserSubscription>.Success(map);

                }
                catch (Exception e)
                {
                    return Result<UserSubscription>.Fail();
                }
            }
            else
            {
                return Result<UserSubscription>.Fail(res.Messages);
            }
        }

        public override async Task<Result<bool>> HasActiveSubscriptionAsync()
        {
            return await Service.HasActiveSubscriptionAsync();
        }
    }


    public class TemplateSubscription : TemplateSubscriptionShare<SubscriptionClientService, DataBuildUserSubscriptionInfo>
    {
   
        public List<UserSubscription> Subscriptions { get => _Subscriptions; }
     
        public List<string> Errors { get => _errors; }


        private List<UserSubscription> _Subscriptions = new List<UserSubscription>();


        public TemplateSubscription(
            IMapper mapper,
            AuthService AuthService,
            SubscriptionClientService client,
            IBuilderSubscriptionComponent<DataBuildUserSubscriptionInfo> builderComponents,
            NavigationManager navigation,
            IDialogService dialogService,
            ISnackbar snackbar) : base(mapper, AuthService, client, builderComponents, navigation, dialogService, snackbar)
        {
            this.BuilderComponents.SubmitCreate = OnSubmitCreateSubscription;

            this.BuilderComponents.SubmitGetAll = OnSubmitGetAllSubscriptions;

            this.BuilderComponents.SubmitPause = OnSubmitPauseSubscription;
            this.BuilderComponents.SubmitResume = OnSubmitUResumeSubscription;
            this.BuilderComponents.SubmitDelete = OnSubmitDeleteSubscription;
        


            this.builderApi = new BuilderSubscriptionApiClient(mapper, client);

          

        }



     
        private async Task OnSubmitDeleteSubscription(DataBuildUserSubscriptionInfo DataBuildUserSubscriptionInfo)
        {

            if (DataBuildUserSubscriptionInfo != null)
            {
                var response = await builderApi.DeleteAsync(DataBuildUserSubscriptionInfo);
                if (response.Succeeded)
                {

                }
                else
                {
                    _errors = response.Messages;
                }
            }

        }
        private async Task OnSubmitPauseSubscription(DataBuildUserSubscriptionInfo DataBuildUserSubscriptionInfo)
        {

            if (DataBuildUserSubscriptionInfo != null)
            {
                var response = await builderApi.PauseAsync(DataBuildUserSubscriptionInfo);
                if (response.Succeeded)
                {

                }
                else
                {
                    _errors = response.Messages;
                }
            }

        }     
        
        private async Task<Result<SubscriptionCreateResponse>> OnSubmitCreateSubscription(DataBuildUserSubscriptionInfo DataBuildUserSubscriptionInfo)
        {


                return await builderApi.CreateAsync(DataBuildUserSubscriptionInfo);
             
            

        }
        private async Task OnSubmitUResumeSubscription(DataBuildUserSubscriptionInfo DataBuildUserSubscriptionInfo)
        {

            if (DataBuildUserSubscriptionInfo != null)
            {
                var response = await builderApi.ResumeAsync(DataBuildUserSubscriptionInfo);
                if (response.Succeeded)
                {

                }
                else
                {
                    _errors = response.Messages;
                }
            }

        }
        private async Task OnSubmitGetAllSubscriptions()
        {

                var response = await builderApi.GetAllAsync();
                if (response.Succeeded)
                {
                    _Subscriptions = response.Data;
                }
                else
                {
                _errors = response.Messages;
            }
        }

        public async Task<Result<List<UserSubscription>>> GetAllSubscriptions()
        {
            return await builderApi.GetAllAsync();
        }

        public async Task<Result<bool>> HasActiveSubscriptionAsync()
        {
            return await builderApi.HasActiveSubscriptionAsync();
        }



    }
}





