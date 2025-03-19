﻿using Blazorise;
using LAHJA.Helpers.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json.Linq;
using Shared.Constants;
using Shared.Constants.Router;


namespace LAHJA
{
    public class BaseInitializationComponent : ComponentBase
    {

        [Inject] public TokenService TokenService { get; set; }
        [Inject] public AuthService AuthService { get; set; }
        [Inject] public ProtectedSessionStorage ProtectedSessionStorage { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }

        public bool IsAuth { get  => (_isAuth) ? getAuthAsync().Result : true; }
                

        private bool _isAuth = false;
       

        private async Task<bool> firstAuthAsync()
        {
            try
            {
                var token = await TokenService.GetTokenAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    await ProtectedSessionStorage.SetAsync(ConstantsApp.ACCESS_TOKEN, token);


                    _isAuth = true;
                    StateHasChanged();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
           
        }
        public async Task<bool> getAuthAsync()
        {
            try
            {
                string?  token  = (await ProtectedSessionStorage.GetAsync<string?>(ConstantsApp.ACCESS_TOKEN)).Value; ;
                if (await AuthService.isAuth() && !string.IsNullOrEmpty(token)&&token!= ConstantsApp.PROTECT_SESSION_DEVAULT_VALUE)
                {
                    //token = await TokenService.GetTokenAsync();
                    //if (!string.IsNullOrEmpty(token))
                    //{
                    //    await ProtectedSessionStorage.SetAsync("accessToken", token);
                    //}
                    _isAuth = true;
                    StateHasChanged();
                    return true;
                }
                else
                {
                    await TokenService.DeleteTokenFromSessionAsync();
                }
               
                return false;

            }
            catch(Exception e)
            {
                return false;
            }
          
               
        }
        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                try
                {

                    //if (!await getAuthAsync())
                    //{
                    //    Navigation.NavigateTo(RouterPage.LOGIN, true);
                    //}


                    //    var token = await firstAuthAsync();

                    //if (string.IsNullOrEmpty(token))
                    //{
                    //    await ProtectedSessionStorage.SetAsync("accessToken", token);

                    //}

                }
                catch
                {

                }
            
              //  StateHasChanged();
            }
        }
    }
}
