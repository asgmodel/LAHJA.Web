
using Blazorise;
using Blazorise.Captcha.ReCaptcha;
using Domain.Entities;
using IdentityModel.Client;
using LAHJA.UI.Components.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using MudBlazor;
using Shared.Constants.Router;
using Shared.Enums;
using Shared.Models;
using Shared.Wrapper;
using System.Text.RegularExpressions;

namespace LAHJA.Data.UI.Components.Base
{

    
   public enum TypeAuth
    {
        Login,
        //ConfirmEmail,
        //ResetPassword,
        //ReSendConfirmEmail,
    }


    public interface IAuthBaseComponentCard
    {
        
    }

    public class DataBuildAuth
    {
      

        private string email = "admin@gmail.com";
        private string password = "Admin123*";

      
    }



    public class DataBuildAuthBase
    {

        public string? FirstName { get; set; } = "ASG";
        public string? LastName { get; set; } = "USER";
        public int? Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; } 
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Picture { get; set; }
        public string? Nationality { get; set; }
        public string? Code { get; set; }
        public bool IsLogin { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }


    public class CardAuth<T> : ComponentBaseCard<T>
    {
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(T db)
        {
            DataBuild = db;
        }


        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter] public EventCallback<ExternalLoginRequest> OnSubmitExternalLogin { get; set; }
        [Parameter] public EventCallback<T> OnSubmit { get; set; }
        [Parameter] public EventCallback<T> OnSubmitConfirmEmail { get; set; }
        [Parameter] public EventCallback<T> OnSubmitReSendConfirmEmail { get; set; }
        [Parameter] public EventCallback<T> OnSubmitResetPassword { get; set; }
        [Parameter] public bool IsLogin { set; get; } = false;
        [Parameter] public EventCallback<T> OnSubmitedForgetPassword { get; set; }
        [Parameter] public List<string> ErrorMessages
        {
            set
            {
                if (value != null && value.Count() > 0)
                    errors = value.ToArray();
            }
        }

        protected bool success;
        protected string[] errors = { };
        protected MudTextField<string> pwField1;
        protected MudForm form;
        protected bool visibleForgetPassword = false;
        protected bool isLoading = false;

        protected string firstName;
        protected string lastName;
        protected string picture = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava3.webp";
        protected string phoneNumber;
        protected string email = "admin@gmail.com";
        protected string password = "Admin123*";
        protected string repeatPassword;
        protected string code;


  
        protected IEnumerable<string> PasswordStrength(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                yield return "Password is required!";
                yield break;


            }
            if (pw.Length < 8)
                yield return "Password must be at least of length 8";
            if (!Regex.IsMatch(pw, @"[A-Z]"))
                yield return "Password must contain at least one capital letter";
            if (!Regex.IsMatch(pw, @"[a-z]"))
                yield return "Password must contain at least one lowercase letter";
            if (!Regex.IsMatch(pw, @"[0-9]"))
                yield return "Password must contain at least one digit";
        }

        protected string PasswordMatch(string arg)
        {
            repeatPassword = pwField1.Value;
            if (pwField1.Value != arg)
                return "Passwords don't match";
            return null;
        }

        protected async Task onSubmitResendConfirmEmail()
        {
            //var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
            //var dialog = DialogService.Show<ConfirmationEmail>("", new DialogParameters(), options);
            //var result = await dialog.Result;
            Navigation.NavigateTo(RouterPage.RE_SEND_CONFIRM_EMAIL, true);
            //if (!result.Cancelled)
            //{
            //    // Handle confirmation
            //}
        }

        protected async Task showResetPassword()
        {
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
            var dialog = DialogService.Show<ResetPasswordComponent>("", new DialogParameters(), options);
            var result = await dialog.Result;

            //if (!result.Cancelled)
            //{
            //    // Handle confirmation
            //}
        }
        protected async Task onClickForgetPassword()
        {

            //visibleForgetPassword = true;
            Navigation.NavigateTo(RouterPage.FORGET_PASSWORD);

            //StateHasChanged();
        }

    

     

     

        protected  void onCloseDialog()
        {
            //MudDialog.Cancel();

            Navigation.NavigateTo(RouterPage.REGISTER,true);

        }
        //protected void onCloseConfirmEmailDialog()
        //{
        //    MudDialog.Cancel();

        //}

      

    }

    public class InputFieldProperties
    {
        public string T { set; get; } = "string";
        public bool Disabled { get; set; } = false;
        public bool Required { get; set; } = true;
        public string Label { get; set; } = string.Empty;
        public string RequiredError { get; set; } = string.Empty;
    }

    public interface ICardInput
    {
        InputFieldProperties Properties { set; get; }
    }
    public class CardInput<T> : ComponentBaseCard<T>, ICardInput
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

        public InputFieldProperties Properties { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Build(T db)
        {
            DataBuild = db;
        }
    }

}