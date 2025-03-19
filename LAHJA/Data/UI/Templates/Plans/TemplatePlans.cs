

using AutoMapper;
using Domain.Entities.Plans.Response;
using Domain.ShareData.Base;
using Domain.Wrapper;
using LAHJA.ApplicationLayer.Plans;
using LAHJA.Data.UI.Components;
using LAHJA.Data.UI.Components.Category;
using LAHJA.Data.UI.Components.Plan;
using LAHJA.Data.UI.Templates.Base;
using LAHJA.Helpers.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Constants.Router;

namespace LAHJA.Data.UI.Templates.Plans
{




    public interface IBuilderPlansComponent<T> : IBuilderComponents<T>
    {
        public Func<T, Task> SubmitContainerPlans { get; set; }
        public Func<T, Task> SubmitCreatePlan { get; set; }
        public Func<T, Task> SubmitUpdatePlan { get; set; }
        public Func<T, Task> SubmitSubscriptionPlan { get; set; }


    }

    public interface IBuilderPlansApi<T> : IBuilderApi<T>
    {

        public Task<Result<List<CategoryComponent>>> GetAllCategories();
        public Task<Result<List<SubscriptionPlanInfo>>> GetPlansAsync(FilterResponseData filter);
        public Task<Result<SubscriptionPlanInfo>> GetPlanAsync(T data);
        public Task<Result<SubscriptionPlanInfo>> UpdatePlanAsync(T data);
        public Task<Result<SubscriptionPlanInfo>> CreatePlanAsync(T data);
        public Task<Result<DeleteResponse>> DeletePlanAsync(T data);


    }

    public abstract class BuilderPlansApi<T, E> : BuilderApi<T, E>, IBuilderPlansApi<E>
    {

        public BuilderPlansApi(IMapper mapper, T service) : base(mapper, service)
        {

        }


        public abstract Task<Result<List<CategoryComponent>>> GetAllCategories();
        public abstract Task<Result<List<SubscriptionPlanInfo>>> GetPlansAsync(FilterResponseData filter);
        public abstract Task<Result<SubscriptionPlanInfo>> GetPlanAsync(E data);

        public abstract Task<Result<SubscriptionPlanInfo>> UpdatePlanAsync(E data);
        public abstract Task<Result<SubscriptionPlanInfo>> CreatePlanAsync(E data);
        public abstract Task<Result<DeleteResponse>> DeletePlanAsync(E data);


    }
    public class BuilderPlansComponent<T> : IBuilderPlansComponent<T>
    {
        public Func<T, Task> SubmitContainerPlans { get; set; }
        public Func<T, Task> SubmitSubscriptionPlan { get; set; }
        public Func<T, Task> SubmitCreatePlan { get; set; }
        public Func<T, Task> SubmitUpdatePlan { get; set; }


    }


    public class TemplatePlansShare<T, E> : TemplateBase<T, E>
    {
        protected readonly NavigationManager navigation;
        protected readonly IDialogService dialogService;
        protected readonly ISnackbar Snackbar;
        protected IBuilderPlansApi<E> builderApi;
        private readonly IBuilderPlansComponent<E> builderComponents;
        public IBuilderPlansComponent<E> BuilderComponents { get => builderComponents; }
        public TemplatePlansShare(

               IMapper mapper,
               AuthService AuthService,
                T client,
                IBuilderPlansComponent<E> builderComponents,
                NavigationManager navigation,
                IDialogService dialogService,
                ISnackbar snackbar


            ) : base(mapper, AuthService, client)
        {



            builderComponents = new BuilderPlansComponent<E>();
            this.navigation = navigation;
            this.dialogService = dialogService;
            this.Snackbar = snackbar;
            //this.builderApi = builderApi;
            this.builderComponents = builderComponents;


        }

    }


    public class BuilderPlansApiClient : BuilderPlansApi<PlansClientService, DataBuildPlansBase>, IBuilderPlansApi<DataBuildPlansBase>
    {
        public BuilderPlansApiClient(IMapper mapper, PlansClientService service) : base(mapper, service)
        {
        }


        public override async Task<Result<SubscriptionPlanInfo>> UpdatePlanAsync(DataBuildPlansBase data)
        {
            var model = Mapper.Map<PlanUpdate>(data);
            var res = await Service.UpdatePlanAsync(model);
            if (res.Succeeded)
            {
                try
                {
                    var map = Mapper.Map<SubscriptionPlanInfo>(res.Data);
                    return Result<SubscriptionPlanInfo>.Success(map);

                }
                catch (Exception e)
                {
                    return Result<SubscriptionPlanInfo>.Fail();
                }
            }
            else
            {
                return Result<SubscriptionPlanInfo>.Fail(res.Messages);
            }
        }

        public override async Task<Result<SubscriptionPlanInfo>> CreatePlanAsync(DataBuildPlansBase data)
        {
            var model = Mapper.Map<PlanCreate>(data);
            var res = await Service.CreatePlanAsync(model);
            if (res.Succeeded)
            {
                try
                {
                    var map = Mapper.Map<SubscriptionPlanInfo>(res.Data);
                    return Result<SubscriptionPlanInfo>.Success(map);

                }
                catch (Exception e)
                {
                    return Result<SubscriptionPlanInfo>.Fail();
                }
            }
            else
            {
                return Result<SubscriptionPlanInfo>.Fail(res.Messages);
            }
        }



        public override async Task<Result<List<SubscriptionPlanInfo>>> GetPlansAsync(FilterResponseData filter)
        {

            var res = await Service.GetPlansAsync(filter);
            if (res.Succeeded)
            {
                var map = Mapper.Map<List<SubscriptionPlanInfo>>(res.Data);
                return Result<List<SubscriptionPlanInfo>>.Success(map);
            }
            else
            {
                return Result<List<SubscriptionPlanInfo>>.Fail(res.Messages);
            }
        }





        public override async Task<Result<SubscriptionPlanInfo>> GetPlanAsync(DataBuildPlansBase data)
        {

            var res = await Service.GetPlanAsync(data.PlanId, data.Lg);
            if (res.Succeeded)
            {
                var map = Mapper.Map<SubscriptionPlanInfo>(res.Data);

                return Result<SubscriptionPlanInfo>.Success(map);
            }
            else
            {
                return Result<SubscriptionPlanInfo>.Fail(res.Messages);
            }
        }





        public override async Task<Result<DeleteResponse>> DeletePlanAsync(DataBuildPlansBase data)
        {

            return await Service.DeletePlanAsync(data.PlanId);

        }

        public override async Task<Result<List<CategoryComponent>>> GetAllCategories()
        {

            var res = await Service.GetCategories(new FilterResponseData());
            if (res.Succeeded)
            {
                try
                {
                    var map = Mapper.Map<List<CategoryComponent>>(res.Data);
                    return Result<List<CategoryComponent>>.Success(map);

                }
                catch (Exception e)
                {
                    return Result<List<CategoryComponent>>.Fail();
                }
            }
            else
            {
                return Result<List<CategoryComponent>>.Fail(res.Messages);
            }
        }
    }


    public class TemplatePlans : TemplatePlansShare<PlansClientService, DataBuildPlansBase>
    {
        public List<CategoryComponent> Categories { get => _categories; }
        public List<SubscriptionPlanInfo> SubscriptionPlans { get => _plans; }
        public List<SubscriptionPlanInfo> AllSubscriptionPlans { get => _allPlans; }
        public SubscriptionPlanInfo SubscriptionPlan { get => _plan; }
        public List<string> Errors { get => _errors; }





        public TemplatePlans(
            IMapper mapper,
            AuthService AuthService,
            PlansClientService client,
            IBuilderPlansComponent<DataBuildPlansBase> builderComponents,
            NavigationManager navigation,
            IDialogService dialogService,
            ISnackbar snackbar) : base(mapper, AuthService, client, builderComponents, navigation, dialogService, snackbar)
        {
            //this.BuilderComponents.SubmitContainerPlans = OnSubmitContainerPlans;
            this.BuilderComponents.SubmitSubscriptionPlan = OnSubmitSubscriptionPlan;
            this.BuilderComponents.SubmitUpdatePlan = OnSubmitUpdatePlans;
            this.BuilderComponents.SubmitCreatePlan = OnSubmitCreatePlans;


            this.builderApi = new BuilderPlansApiClient(mapper, client);

            Task.FromResult(OnInitialize());

        }



        private List<CategoryComponent> _categories = new List<CategoryComponent>();
        private List<SubscriptionPlanInfo> _plans = new List<SubscriptionPlanInfo>();
        private List<SubscriptionPlanInfo> _allPlans = new List<SubscriptionPlanInfo>();
        private SubscriptionPlanInfo _plan = new SubscriptionPlanInfo();

        //public  IBuilderPlansComponent<DataBuildPlansBase, DataBuildPlansBase> BuilderPlansComponent { get => builderPlansComponents; }

        public async Task OnInitialize()
        {

            var response = await builderApi.GetAllCategories();
            if (response.Succeeded)
            {
                _categories=response.Data;  
            }
            else
            {
                _errors = response.Messages;
            }

        }
        public async Task getAllSubscriptionsPlansAsync(int take = 0, int singleDistinctionNum = 0)
        {

            var response = await builderApi.GetPlansAsync(new FilterResponseData { lg = "ar" });
            if (response.Succeeded)
            {
                if (singleDistinctionNum > 0 && take > 0)
                {
                    _allPlans = response.Data.Take(take).ToList();
                    if (_allPlans.Count() > singleDistinctionNum)
                    {
                        _allPlans[singleDistinctionNum].ClassImport = "plan-import-card";
                        _allPlans[singleDistinctionNum].HeaderImport = "textHeader";
                    }
                }
                else
                {

                    _allPlans = response.Data;
                }

        ;
            }
            else
            {
                _errors = response.Messages;
            }


        }
        private async Task OnSubmitCreatePlans(DataBuildPlansBase dataBuildPlansBase)
        {

            if (dataBuildPlansBase != null)
            {
                var response = await builderApi.CreatePlanAsync(dataBuildPlansBase);
                if (response.Succeeded)
                {

                }
                else
                {
                    _errors = response.Messages;
                }
            }

        }

        private async Task OnSubmitUpdatePlans(DataBuildPlansBase dataBuildPlansBase)
        {

            if (dataBuildPlansBase != null)
            {
                var response = await builderApi.UpdatePlanAsync(dataBuildPlansBase);
                if (response.Succeeded)
                {

                }
                else
                {
                    _errors = response.Messages;
                }
            }

        }


        public async Task OnSubmitSubscriptionPlan(DataBuildPlansBase dataBuildPlansBase)
        {
            var isAuth = await authService.isAuth();
            if (isAuth && dataBuildPlansBase != null)
            {
                navigation.NavigateTo($"{RouterPage.PAYMENT}/{dataBuildPlansBase.PlanId}");
            }
            else
            {
                navigation.NavigateTo(RouterPage.LOGIN);
            }

        }
        public async Task<Result<SubscriptionPlanInfo>> GetSubmitSubscriptionPlan(DataBuildPlansBase dataBuildPlansBase)
        {


            var response = await builderApi.GetPlanAsync(dataBuildPlansBase);

            return response;



        }


    }

}

