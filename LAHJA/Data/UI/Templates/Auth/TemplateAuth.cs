using Application.UseCase.Auth;
using AutoMapper;
using Blazorise;
using Domain.Entities;
using Domain.Entities.Auth.Request;
using Domain.Entities.Auth.Response;
using Domain.ShareData;
using Domain.Wrapper;
using LAHJA.ApplicationLayer.Auth;
using LAHJA.Data.UI.Components.Base;
using LAHJA.Data.UI.Templates.Base;
using LAHJA.Helpers;
using LAHJA.Helpers.Enum;
using LAHJA.Helpers.Services;
using LAHJA.UI.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Constants;
using Shared.Constants.Router;
using Shared.Helpers;
using Shared.Wrapper;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LAHJA.Data.UI.Templates.Auth;


public enum TypeTemplateAuth
{

}


//public interface ITemplateAuth : ITemplateBase<DataBuildAuthBase, LoginResponse>
//{


//}

//}//public interface ITemplateAuth : ITemplateBase<DataBuildAuthBase,LoginResponse>
//{

//}
//public class TemplateAuth: TemplateBase<DataBuildAuthBase, LoginResponse>, ITemplateAuth
//{


//    public EventCallback<DataBuildAuthBase> OnSubmit { get; set; }

//    public EventCallback<string> OnSubmitedForgetPassword { get; set; }

//    public override DataBuildAuthBase Map(LoginResponse data)
//    {
//        throw new NotImplementedException();
//    }


//}
public interface IBuilderAuthComponent<T>: IBuilderComponents<T>
{
    public Func<ExternalLoginRequest, Task> SubmitExternalLogin { get; set; }
    public Func<T, Task> Submit { get; set; }
    public Func<T, Task> SubmitedForgetPassword { get; set; }
    public Func<T, Task> SubmitConfirmEmail { get; set; }
    public Func<T, Task> SubmitReSendConfirmEmail { get; set; }
    public Func<T, Task> SubmitResetPassword { get; set; }
    public Func<T, Task> SubmitLogout { get; set; }


}



public interface IBuilderAuthApi<T> : IBuilderApi<T>
{

     Task<Result<LoginResponse>> Login(T data);
     Task ExternalLoginAsync(ExternalLoginRequest data);
    Task<Result<AccessTokenResponse>> RefreshToken(RefreshRequest request);
    Task<Result<string>> Logout();
     Task<Result<RegisterResponse>> Register(T data);
     Task<Result<ResetPasswordResponse>> ResetPassword(T data);
     Task<Result> ReSendConfirmationEmail(T data);
     Task<Result> SubmitConfirmEmail(T data);
     Task<Result> ForgetPassword(T data);
    

}

public abstract class BuilderAuthApi<T,E> : BuilderApi<T,E>, IBuilderAuthApi<E> 
{

    public BuilderAuthApi(IMapper mapper, T service) : base(mapper,service)
    {
      
    }

    public abstract Task ExternalLoginAsync(ExternalLoginRequest data);
    public abstract Task<Result> ForgetPassword(E data);

    public abstract Task<Result<LoginResponse>> Login(E data);
    public abstract Task<Result<string>> Logout();

    public abstract Task<Result<RegisterResponse>> Register(E data);
    public abstract Task<Result<AccessTokenResponse>> RefreshToken(RefreshRequest request);


    public abstract Task<Result> ReSendConfirmationEmail(E data);

    public abstract Task<Result<ResetPasswordResponse>> ResetPassword(E data);

    public abstract Task<Result> SubmitConfirmEmail(E data);
   
}
public class BuilderAuthComponent<T> : IBuilderAuthComponent<T>
{

    public Func<ExternalLoginRequest, Task> SubmitExternalLogin { get; set; }
    public Func<T, Task> Submit { get ; set ; }
    public Func<T, Task> SubmitedForgetPassword { get; set; }
    public Func<T, Task> SubmitConfirmEmail { get; set; }
    public Func<T, Task> SubmitReSendConfirmEmail { get; set; }
    public Func<T, Task> SubmitResetPassword { get; set; }
    public Func<T, Task> SubmitLogout { get; set; }

}


public class TemplateAuthShare<T,E> : TemplateBase<T,E>
{
    protected readonly NavigationManager navigation;
    protected readonly IDialogService dialogService;
    protected readonly ISnackbar Snackbar;
    protected IBuilderAuthApi<E> builderApi;
    protected readonly CustomAuthenticationStateProvider AuthStateProvider;
    //protected readonly ProtectedSessionStorage  PSession;

    private readonly IBuilderAuthComponent<E> builderComponents;
    public  IBuilderAuthComponent<E> BuilderComponents { get => builderComponents; }
    public TemplateAuthShare(

           IMapper mapper,
           CustomAuthenticationStateProvider authStateProvider,
           AuthService authService,
            //ProtectedSessionStorage PSession,
            T client,
            IBuilderAuthComponent<E> builderComponents,
            NavigationManager navigation,
            IDialogService dialogService,
            ISnackbar snackbar ) : base(mapper, authService, client)
    {



        builderComponents = new BuilderAuthComponent<E>();
        this.navigation = navigation;
        this.dialogService = dialogService;
        this.Snackbar = snackbar;
        //this.builderApi = builderApi;
        this.builderComponents = builderComponents;
        AuthStateProvider = authStateProvider;
    }

}


public class BuilderAuthApiClient : BuilderAuthApi<ClientAuthService, DataBuildAuthBase>, IBuilderAuthApi<DataBuildAuthBase>
{

    private readonly NavigationManager navigationManager;
    public BuilderAuthApiClient(IMapper mapper, ClientAuthService service, NavigationManager navigationManager) : base(mapper, service)
    {
        this.navigationManager = navigationManager;
    }

    public override  async Task ExternalLoginAsync(ExternalLoginRequest request)
    {
        await Service.ExternalLoginAsync(request);

    }
    public override async Task<Result> ForgetPassword(DataBuildAuthBase data)
    {
        var model = Mapper.Map<ForgetPasswordRequest>(data);

        model.ReturnUrl = Helper.GetInstance().GetFullPath(ConstantsApp.RESET_PASSWORDL_PAGE_URL);// navigationManager.BaseUri+ConstantsApp.RESET_PASSWORDL_PAGE_URL;
        return await Service.forgetPasswordAsync(model);
    }

    public override async Task<Result<LoginResponse>> Login(DataBuildAuthBase data)
    {
        var model = Mapper.Map<LoginRequest>(data);

         return await Service.loginAsync(model);
       
    } 
    
    public override async Task<Result<string>> Logout()
    {
        

         return await Service.logoutAsync();
       
    }

    public async override Task<Result<AccessTokenResponse>> RefreshToken(RefreshRequest request)
    {
        return await Service.RefreshToken(request);
    }

    public override async Task<Result<RegisterResponse>> Register(DataBuildAuthBase data)
    {
        var model = Mapper.Map<RegisterRequest>(data);

        model.ReturnUrl = Helper.GetInstance().GetFullPath(ConstantsApp.CONFIRM_EMAIL_PAGE_URL); //navigationManager.BaseUri + ConstantsApp.CONFIRM_EMAIL_PAGE_URL;


        return await Service.registerAsync(model);
    }

    public override async Task<Result> ReSendConfirmationEmail(DataBuildAuthBase data)
    {
        var model = Mapper.Map<ResendConfirmationEmail>(data);

        model.ReturnUrl = Helper.GetInstance().GetFullPath(ConstantsApp.CONFIRM_EMAIL_PAGE_URL); //navigationManager.BaseUri+ConstantsApp.CONFIRM_EMAIL_PAGE_URL;

        return await Service.reConfirmationEmailAsync(model);
    }

    public override async Task<Result<ResetPasswordResponse>> ResetPassword(DataBuildAuthBase data)
    {
        var model = Mapper.Map<ResetPasswordRequest>(data);

        return await Service.resetPasswordAsync(model);
    }

    public override async Task<Result> SubmitConfirmEmail(DataBuildAuthBase data)
    {
        var model = Mapper.Map<ConfirmationEmail>(data);
        model.ReturnUrl = Helper.GetInstance().GetFullPath(ConstantsApp.CONFIRM_EMAIL_PAGE_URL);
        //navigationManager.BaseUri + ConstantsApp.CONFIRM_EMAIL_PAGE_URL;

        return await Service.confirmationEmailAsync(model);
    }
}


public class TemplateAuth: TemplateAuthShare<ClientAuthService, DataBuildAuthBase>
{


    private readonly ISessionUserManager sessionUserManager;
    //private readonly ISessionUserManager sessionUserManager;
    public TemplateAuth(IMapper mapper,
        AuthService authService,
        ClientAuthService client,
        CustomAuthenticationStateProvider AuthStateProvider,
        IBuilderAuthComponent<DataBuildAuthBase> builderComponents,
        NavigationManager navigation,
        IDialogService dialogService,
        ISnackbar snackbar,
        ISessionUserManager sessionUserManager) : base(mapper, AuthStateProvider, authService, client, builderComponents, navigation, dialogService, snackbar)
    {
        this.BuilderComponents.SubmitExternalLogin = OnSubmitExternalLogin;
        this.BuilderComponents.Submit = OnSubmit;
        this.BuilderComponents.SubmitLogout = OnSubmitLogout;
        this.BuilderComponents.SubmitConfirmEmail = OnSubmitConfirmEmail;
        this.BuilderComponents.SubmitReSendConfirmEmail = OnReSendConfirmationEmail;
        this.BuilderComponents.SubmitResetPassword = OnResetPassword;
        this.BuilderComponents.SubmitedForgetPassword = OnSubmitForgetPasswordAsync;
        this.builderApi = new BuilderAuthApiClient(mapper, client, navigation);
        this.sessionUserManager = sessionUserManager;
    }



    public List<string> Errors { get => _errors; }


    //public  IBuilderAuthComponent<DataBuildAuthBase, DataBuildAuthBase> BuilderAuthComponent { get => builderAuthComponents; }


    public async Task LogoutAsync()
    {
        await OnSubmitLogout();
    }

    public async Task<Result<AccessTokenResponse>> RefreshToken(RefreshRequest request)
    {
        var response = await builderApi.RefreshToken(request);
        if (response.Succeeded)
        {
            var model = mapper.Map<LoginResponse>(response.Data);
            await authService.SaveLoginAsync(model);
            
        }
        return response;
    }

 
    private async Task<bool> ConfirmAsync(string title ,string message)
    {
        var parameters = new DialogParameters<DialogBox>
        {
            { x => x.Title, title },
            { x => x.Message,message},
            //{ x => x.Color, Color.Success }
        };
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
        var dialog = dialogService.Show<DialogBox>(" ", parameters, options);


        var result = await dialog.Result;
        return !result.Canceled;
        

    }
    public async Task ReForgetPassword(DataBuildAuthBase data)
    {
        var response = await builderApi.ForgetPassword(data);
        if (response.Succeeded)
        {
            var msg = MapperMessages.Map(SuccessMessages.LINK_SENT_SUCCESSFULLY_EN, SuccessMessages.LINK_SENT_SUCCESSFULLY_AR);
            Snackbar.Add(msg, Severity.Success);
        }
        else
        {
            //if (response.Messages != null && response.Messages.Count() > 0)
            {
                var msg = MapperMessages.Map(ErrorMessages.PROCESS_IS_FAILED_EN, ErrorMessages.PROCESS_IS_FAILED_AR);
                Snackbar.Add(msg, Severity.Success);

            }
        }
    }

    public async Task ReSendConfirmationEmail(DataBuildAuthBase data)
    {
        var response = await builderApi.ReSendConfirmationEmail(data);
        if (response.Succeeded)
        {
            var msg = MapperMessages.Map(SuccessMessages.LINK_SENT_SUCCESSFULLY_EN, SuccessMessages.LINK_SENT_SUCCESSFULLY_AR);
            Snackbar.Add(msg, Severity.Success);
        }
        else
        {
            //if (response.Messages != null && response.Messages.Count() > 0)
            {
                var msg = MapperMessages.Map(ErrorMessages.PROCESS_IS_FAILED_EN, ErrorMessages.PROCESS_IS_FAILED_AR);
                Snackbar.Add(msg,Severity.Success);

            }
        }
    }
    public async Task OnReSendConfirmationEmail(DataBuildAuthBase data)
    {
      

        var response = await builderApi.ReSendConfirmationEmail(data);
        if (response.Succeeded)
        {


            var fullPath = Helper.GetInstance().GetFullPath(ConstantsApp.CONFIRM_EMAIL_PAGE_URL);
            navigation.NavigateTo($"{RouterPage.EMAIL_CONFIRM_PAGE}?Email={data.Email}&Url={fullPath}&Method={AuthMethods.ConfirmEmail.ToString()}", forceLoad: true);

            //var res = await  ConfirmAsync("Confirm Email", SuccessMessages.CONFIRM_EMAIL_MESSAGE_EN);
            // if (res == true)
            //{
            //        navigation.NavigateTo(RouterPage.LOGIN, forceLoad: true);
            //}
            //else
            //{

            //}


        }
        else
        {
            var res = await ConfirmAsync("Error", ErrorMessages.PROCESS_IS_FAILED_EN);
    
        }
    }

    protected async Task OnResetPassword(DataBuildAuthBase dataBuildAuthBase)
    {
        var response = await builderApi.ResetPassword(dataBuildAuthBase);
        if (response.Succeeded)
        {

            navigation.NavigateTo(RouterPage.LOGIN, forceLoad: true);

        }
        else
        {
            if (response.Messages != null && response.Messages.Count() > 0)
            {
                var msg = MapperMessages.Map(ErrorMessages.INVALID_EMAIL_EN, ErrorMessages.IINVALID_EMAIL_AR);
                _errors?.Clear();
                _errors.Add(msg);
                Snackbar.Add(msg, Severity.Error);
            }
        };
    }

    protected async Task OnSubmitConfirmEmail(DataBuildAuthBase dataBuildAuthBase)
    {

        var response = await builderApi.SubmitConfirmEmail(dataBuildAuthBase);
        if (response.Succeeded)
        {

            var res = await ConfirmAsync("Confirm Email", SuccessMessages.CONFIRM_EMAIL_MESSAGE_EN);
            if (res == true)
            {
                navigation.NavigateTo(RouterPage.LOGIN, forceLoad: true);
            }
            
          

        }
        else
        {

            var res = await ConfirmAsync("Error", ErrorMessages.PROCESS_IS_FAILED_EN);

        }
    }

    private async Task OnSubmitExternalLogin(ExternalLoginRequest request)
    {

        try
        {
            request.ReturnUrl = Helper.GetInstance().GetFullPath(ConstantsApp.RETEURN_EXTERNAL_LOGIN_PAGE); 
            //$"{navigation.BaseUri}ReturnExternalLoginPage";
            //await builderApi.ExternalLoginAsync(request);
            navigation.NavigateTo($"https://asg-api.runasp.net/api/ExternalLogin?provider={request.Provider}&returnUrl={request.ReturnUrl}",true);

        }
        catch(Exception e){

            Snackbar.Add(e.Message, Severity.Error);
        }

          
       

    }
    private async Task OnSubmit(DataBuildAuthBase dataBuildAuthBase)
    {

        if (dataBuildAuthBase != null)
        {
            if (dataBuildAuthBase.IsLogin)
            {

               
                await handleApiLoginAsync(dataBuildAuthBase);

            }
            else
            {
         
                await handleApiRegisterAsync(dataBuildAuthBase);
            }
        }

    }

    
    private async Task OnSubmitForgetPasswordAsync(DataBuildAuthBase data)
    {
        var response = await builderApi.ForgetPassword(data);
        if (response.Succeeded)
        {
            var fullPath = Helper.GetInstance().GetFullPath(ConstantsApp.RESET_PASSWORDL_PAGE_URL);
            navigation.NavigateTo($"{RouterPage.EMAIL_CONFIRM_PAGE}?Email={data.Email}&Url={fullPath}&Method={AuthMethods.ForgetPassword.ToString()}", forceLoad: true);
            //navigation.NavigateTo($"{RouterPage.FORGET_PASSWORD}/{SuccessMessages.LINK_SENT_SUCCESSFULLY_AR}", forceLoad: true);

        }
        else
        {
            if (response.Messages != null && response.Messages.Count() > 0)
            {

                _errors?.Clear();
                var msg = MapperMessages.Map(ErrorMessages.INVALID_EMAIL_EN, ErrorMessages.IINVALID_EMAIL_AR); 
                Snackbar.Add(msg, Severity.Error);
                _errors.Add(msg);

            }
        }
    }
    private async Task handleApiLoginAsync(DataBuildAuthBase date)
    {
        var response =await builderApi.Login(date);


        if (response.Succeeded)
        {

            try
            {
                await sessionUserManager.StoreEmailAsync(date.Email);
              
            }
            finally
            {
           
                await authService.SaveLoginAsync(response.Data);
                await authService.SaveLoginTypeAsync(LoginType.Email);
                await AuthStateProvider.InitializeAsync();
                navigation.NavigateTo(RouterPage.HOME, forceLoad: true);
            }

        }
        else
        {
            if (response.Messages != null && response.Messages.Count() > 0)
            {
                // errorMessages.AddRange(response.Messages);
                _errors.Clear();
                if (response.Messages.Count > 0)
                {
                    if (response.Messages[0].Contains("Unauthorized"))
                        _errors.Add(MapperMessages.Map(ErrorMessages.INVALID_EMAIL_EN, ErrorMessages.IINVALID_EMAIL_AR));
                    else
                        _errors.AddRange(response.Messages);
                }
            }

            //navigation.NavigateTo(RouterPage.LOGIN, forceLoad: true);
        }
    }
    private async Task handleApiRegisterAsync(DataBuildAuthBase data)
    {
        var response = await builderApi.Register(data);
        if (response.Succeeded)
        {
            var fullPath = Helper.GetInstance().GetFullPath(ConstantsApp.CONFIRM_EMAIL_PAGE_URL);
            navigation.NavigateTo($"{RouterPage.EMAIL_CONFIRM_PAGE}?Email={data.Email}&Url={fullPath}&Method={AuthMethods.ConfirmEmail.ToString()}", forceLoad: true);

        }
        else
        {
            if (response.Messages != null && response.Messages.Count() > 0)
            {
                _errors.Clear();
                if (response.Messages.Count > 0)
                {
                    if (response.Messages[0].Contains("Unauthorized"))
                        _errors.Add(MapperMessages.Map(ErrorMessages.INVALID_EMAIL_EN, ErrorMessages.IINVALID_EMAIL_AR));
                    else
                        _errors.AddRange(response.Messages);

                    Snackbar.Add(response.Messages[0], Severity.Error);
                }
            }
        }
    }

    private async Task OnSubmitLogout(DataBuildAuthBase? dataBuildAuthBase = null)
    {
        try
        {
            var response = await builderApi.Logout();
            if (response.Succeeded){ }
            else{ }

            await authService.DeleteLoginAsync();
        }
        catch (Exception e)
        {
            //Snackbar.Add();
        }
        finally
        {
            //await AuthStateProvider.InitializeAsync();
            AuthStateProvider.MarkUserAsLoggedOut();
            navigation.NavigateTo(RouterPage.HOME, forceLoad: true);
        }
    }

}








//public class TemplateAuth
//{

//    [Inject] public ClientAuthService ClientAuthService { get; set; }
//    [Inject] public IMapper Mapper { get; set; }
//    [Inject] public NavigationManager Navigation { get; set; }

//    [Inject] public AppCustomAuthenticationStateProvider appCustomAuthenticationStateProvider { get; set; }

//    //private readonly ClientAuthService _clientAuthService;
//    //private readonly IMapper mapper;
//    //private readonly NavigationManager Navigation;
//    //private readonly AppCustomAuthenticationStateProvider appCustomAuthenticationStateProvider;


//    public Func<DataBuildAuthBase, Task> OnSubmit { get; set; }
//    public Func<string, Task> OnSubmitedForgetPassword { get; set; }
//    public List<string> Errors { get => _errors; }

//    private List<string> _errors=new List<string>();


//    public TemplateAuth(
//        //ClientAuthService clientAuthService, 
//        //IMapper mapper,
//        //NavigationManager navigation,
//        //AppCustomAuthenticationStateProvider appCustomAuthenticationStateProvider
//        )
//    {
//        OnSubmit = handleSubmitAsync;
//        OnSubmitedForgetPassword = handleSubmitForgetPasswordAsync;
//        //this._clientAuthService = clientAuthService;
//        //mapper = mapper;
//        //this.Navigation = navigation;
//        //this.appCustomAuthenticationStateProvider = appCustomAuthenticationStateProvider;
//        //_errors = new List<string>();
//    }

////private LoginResponse Map(DataBuildAuthBase data)
////{
////    return mapper.Map<LoginResponse>(data);
////}

////private DataBuildAuthBase Map(LoginResponse data)
////{
////    return mapper.Map<DataBuildAuthBase>(data);
////}


//private async Task handleSubmitAsync(DataBuildAuthBase dataBuildAuthBase)
//{

//    if (dataBuildAuthBase != null)
//    {
//        if (dataBuildAuthBase.IsLogin)
//        {
//            var data = Mapper.Map<LoginRequest>(dataBuildAuthBase);
//            await handleApiLoginAsync(data);

//        }
//        else
//        {
//            var data = Mapper.Map<RegisterRequest>(dataBuildAuthBase);
//            await handleApiRegisterAsync(data);
//        }
//    }

//}
//private async Task handleSubmitForgetPasswordAsync(string email)
//{
//    var response = await ClientAuthService.forgetPasswordAsync(email);
//    if (response.Succeeded)
//    {

//        //Navigation.NavigateTo(RouterPage.HOME, forceLoad: true);

//    }
//    else
//    {
//        if (response.Messages != null && response.Messages.Count() > 0)
//        {

//            _errors?.Clear();
//            _errors.Add(MapperMessages.Map(ErrorMessages.INVALID_EMAIL_EN, ErrorMessages.IINVALID_EMAIL_AR));

//        }
//    }
//}
//private async Task handleApiLoginAsync(LoginRequest date)
//{
//    var response = await ClientAuthService.loginAsync(date);
//    if (response.Succeeded)
//    {
//        // toLoginMode = true;

//        //appCustomAuthenticationStateProvider.StoreAuthenticationData(response.Data.accessToken);
//        Navigation.NavigateTo(RouterPage.HOME, forceLoad: true);

//    }
//    else
//    {
//        if (response.Messages != null && response.Messages.Count() > 0)
//        {
//            // errorMessages.AddRange(response.Messages);
//            _errors?.Clear();
//            _errors.Add(MapperMessages.Map(ErrorMessages.INVALID_EMAIL_EN, ErrorMessages.IINVALID_EMAIL_AR));
//            //Snackbar.Add(response.Messages[0], Severity.Error);
//        }
//    }
//}
//private async Task handleApiRegisterAsync(RegisterRequest data)
//{
//    var response = await ClientAuthService.registerAsync(data);
//    if (response.Succeeded)
//    {
//        // toLoginMode = true;
//        Navigation.NavigateTo(RouterPage.LOGIN, forceLoad: true);

//        //ChangeAuth();

//    }
//    else
//    {
//        if (response.Messages != null && response.Messages.Count() > 0)
//        {
//            // errorMessages.AddRange(response.Messages);
//            _errors.Clear();
//            _errors.Add(MapperMessages.Map(ErrorMessages.INVALID_EMAIL_EN, ErrorMessages.IINVALID_EMAIL_AR));
//            //Snackbar.Add(response.Messages[0], Severity.Error);
//        }
//    }
//}



//}