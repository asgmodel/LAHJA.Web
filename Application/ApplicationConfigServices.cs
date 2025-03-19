using Application.Services.Auth;
using Application.Services.AuthorizationSession;
using Application.Services.Checkout;
using Application.Services.ModelAi;
using Application.Services.Plans;
using Application.Services.Profile;
using Application.Services.Prroduct;
using Application.Services.Service;
using Application.Services.Subscriptions;
using Application.UseCase;
using Application.UseCase.Auth;
using Application.UseCase.AuthorizationSession;
using Application.UseCase.ModelAi;
using Application.UseCase.Plans;
using Application.UseCase.Plans.Get;
using Application.UseCase.Request;
using Application.UseCase.Service;
using Application.UseCase.Space;
using Domain.ShareData.Base;
using Infrastructure.DataSource.ApiClient.Plans;
using Infrastructure.Mappings.Plans;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ApplicationConfigServices
    {
        public static void InstallApplicationConfigServices(this IServiceCollection serviceCollection)
        {

            InstallMapping(serviceCollection);
            InstallUsaCases(serviceCollection);
            InstallServices(serviceCollection);

        }


       private static  void InstallMapping(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(ApplicationMappingConfig));
        }

        private static void InstallUsaCases(this IServiceCollection serviceCollection)
        {
            /// Auth
            serviceCollection.AddScoped<LoginUseCase>();
            serviceCollection.AddScoped<ExternalLoginUseCase>();
            serviceCollection.AddScoped<RegisterUseCase>();
            serviceCollection.AddScoped<ForgetPasswordUseCase>();
            serviceCollection.AddScoped<ResetPasswordUseCase>();
            serviceCollection.AddScoped<ConfirmationEmailUseCase>();
            serviceCollection.AddScoped<ReSendConfirmationEmailUseCase>();
            serviceCollection.AddScoped<RefreshTokinUseCase>();
            serviceCollection.AddScoped<LogoutUseCase>();
            serviceCollection.AddScoped<GetProfileUserUseCase>();

            /// Plans
            /// Active
            serviceCollection.AddScoped<GetPlansUseCase>();
            serviceCollection.AddScoped<GetPlanUseCase>();
            serviceCollection.AddScoped<CreatePlanUseCase>();
            serviceCollection.AddScoped<UpdatePlanUseCase>();
            serviceCollection.AddScoped<DeletePlanUseCase>();

            /// No Active
            serviceCollection.AddScoped<GetAllContainersPlansUseCase>();
            serviceCollection.AddScoped<GetPlansGroupUseCase>();
            serviceCollection.AddScoped<GetAllPlansUseCase>();
            serviceCollection.AddScoped<UpdatePlanUseCase>();
            serviceCollection.AddScoped<CreatePlanUseCase>();
            serviceCollection.AddScoped<GetPlanByIdUseCase>();
            serviceCollection.AddScoped<GetPlanInfoByIdUseCase>();
            serviceCollection.AddScoped<GetAllPlansContainersUseCase>();
            serviceCollection.AddScoped<GetSubscriptionPlanFeaturesUseCase>();
            serviceCollection.AddScoped<GetPlansUseCase>();
         


            /// Profile
            serviceCollection.AddScoped<GetProfileUseCase>();
            serviceCollection.AddScoped<CreateProfileUseCase>();
            serviceCollection.AddScoped<UpdateProfileUseCase>();
            serviceCollection.AddScoped<DeleteProfileUseCase>();
            serviceCollection.AddScoped<UpdateProfileUserUseCase>();


            /// Payment
            serviceCollection.AddScoped<CheckoutUseCase>();
            serviceCollection.AddScoped<CheckoutManageUseCase>();

               
            /// Price
            serviceCollection.AddScoped<SearchPriceUseCase>();
            serviceCollection.AddScoped<DeletePriceUseCase>();
            serviceCollection.AddScoped<CreatePriceUseCase>();
            serviceCollection.AddScoped<UpdatePriceUseCase>();


            /// Product
            serviceCollection.AddScoped<CreateProductUseCase>();
            serviceCollection.AddScoped<UpdateProductUseCase>();
            serviceCollection.AddScoped<DeleteProductUseCase>();
            serviceCollection.AddScoped<GetAllProductsUseCase>();
            serviceCollection.AddScoped<SearchProductUseCase>();




            /// Settings
            //serviceCollection.AddScoped<SearchPriceUseCase>();


            /// Subscription
            serviceCollection.AddScoped<HasActiveSubscriptionUseCase>();
            serviceCollection.AddScoped<PauseSubscriptionUseCase>();
            serviceCollection.AddScoped<DeleteSubscriptionUseCase>();
            serviceCollection.AddScoped<ResumeSubscriptionUseCase>();
            serviceCollection.AddScoped<GetAllSubscriptionsUseCase>();
            serviceCollection.AddScoped<CreateSubscriptionUseCase>();
            serviceCollection.AddScoped<UpdateSubscriptionUseCase>();

            //// Credit Card
            serviceCollection.AddScoped<ActiveCreditCardUseCase>();
            serviceCollection.AddScoped<GetCreditCardsUseCase>();
            serviceCollection.AddScoped<CreateCreditCardUseCase>();
            serviceCollection.AddScoped<UpdateCreditCardUseCase>();
            serviceCollection.AddScoped<DeleteCreditCardUseCase>();

            //// Billing
            serviceCollection.AddScoped<GetBillingDetailsUseCase>();
            serviceCollection.AddScoped<CreateBillingDetailsUseCase>();
            serviceCollection.AddScoped<UpdateBillingDetailsUseCase>();
            serviceCollection.AddScoped<DeleteBillingDetailsUseCase>();

            // Services
            serviceCollection.AddScoped<GetAllServicesUseCase>();
            serviceCollection.AddScoped<GetServiceByIdUseCase>();
            serviceCollection.AddScoped<CreateServiceUseCase>();
            serviceCollection.AddScoped<UpdateServiceUseCase>();
            serviceCollection.AddScoped<DeleteServiceUseCase>();

            // Request
            serviceCollection.AddScoped<CreateRequestUseCase>();
            serviceCollection.AddScoped<RequestAllowedUseCase>();
            serviceCollection.AddScoped<CreateEventUseCase>();
            serviceCollection.AddScoped<ResultRequestUseCase>();


            // Space
            serviceCollection.AddScoped<CreateSpaceAuthorizationUseCase>();


            // AuthorizationSession
            serviceCollection.AddScoped<GetSessionsAccessTokensUseCase>();
            serviceCollection.AddScoped<CreateAuthorizationSessionUseCase>();
            serviceCollection.AddScoped<AuthorizationSessionUseCase>();
            serviceCollection.AddScoped<ValidateSessionTokenUseCase>();
            serviceCollection.AddScoped<EncryptFromWebUseCase>();
            serviceCollection.AddScoped<EncryptFromCoreUseCase>();
            serviceCollection.AddScoped<DeleteAuthorizationSessionUseCase>();
            serviceCollection.AddScoped<ResumeSessionTokenUseCase>();
            serviceCollection.AddScoped<PauseSessionTokenUseCase>();


            // ModelAi
       
            serviceCollection.AddScoped<GetModelsByCategoryUseCase>();
            serviceCollection.AddScoped<GetModelAiUseCase>();
            serviceCollection.AddScoped<GetStartStudioUseCase>();
            serviceCollection.AddScoped<GetValueFilterServiceUseCase>();
            serviceCollection.AddScoped<GetSettingModelAiUseCase>();
            serviceCollection.AddScoped<GetModelChatStudioUseCase>();
            serviceCollection.AddScoped<GetModelsAiUseCase>();
            serviceCollection.AddScoped<GetModelsByDialectUseCase>();
            serviceCollection.AddScoped<GetModelsByGenderUseCase>();
            serviceCollection.AddScoped<GetModelsByLanguageUseCase>();
            serviceCollection.AddScoped<GetModelsByIsStandardUseCase>();
            serviceCollection.AddScoped<GetModelsByLanguageAndDialectUseCase>();
            serviceCollection.AddScoped<GetModelsByTypeAndGenderUseCase>();
            serviceCollection.AddScoped<GetModelsByLanguageDialectTypeUseCase>();
            serviceCollection.AddScoped<GetModelSpeechStudioUseCase>();
            serviceCollection.AddScoped<GetModelTextStudioUseCase>();



        }

        private static void InstallServices(this IServiceCollection serviceCollection)
        {
           
            serviceCollection.AddScoped<PlansService>();
            serviceCollection.AddScoped<WebAuthService>();
            serviceCollection.AddScoped<ProfileService>();
            serviceCollection.AddScoped<CheckoutService>();
            serviceCollection.AddScoped<PriceService>();
            serviceCollection.AddScoped<ProductService>();
            serviceCollection.AddScoped<SubscriptionService>();
            serviceCollection.AddScoped<BillingService>();
            serviceCollection.AddScoped<CreditCardService>();
            serviceCollection.AddScoped<LAHJAService>();
            serviceCollection.AddScoped<RequestService>();
            serviceCollection.AddScoped<SpaceService>();
            serviceCollection.AddScoped<AuthorizationSessionService>();
            serviceCollection.AddScoped<ModelAiService>();

        }

    }
}
