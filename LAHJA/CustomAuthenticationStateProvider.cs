using Domain.Entities.Auth.Request;
using IdentityModel;
using LAHJA.Data.UI.Templates.Auth;
using LAHJA.Helpers.Enum;
using LAHJA.Helpers.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;

namespace LAHJA
{

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly TokenService _tokenService;
  
        private readonly AuthenticationState _anonymous;

        private bool _isInitialized = false;
        public CustomAuthenticationStateProvider(TokenService localStorage)
        {
            _tokenService = localStorage;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            // إذا لم يكن التطبيق جاهزًا لاستدعاء JavaScript، أرجع مستخدم مجهول مؤقتًا
            if (!_isInitialized)
                return _anonymous;


            var token = await _tokenService.GetTokenAsync();

            if (string.IsNullOrEmpty(token))
                return _anonymous;

           return  AuthenticatedState(token);
        }
        //public async Task StartUpWebAsync()
        //{

        //}
        public async Task InitializeAsync()
        {
            var token = await _tokenService.GetTokenAsync();
            _isInitialized = true;
            if (!string.IsNullOrEmpty(token))
            {           
                await _tokenService.SaveTokenInSessionAsync(token);
                MarkUserAsAuthenticated(token);
            }
            else
            {
                await _tokenService.DeleteTokenFromSessionAsync();
                MarkUserAsLoggedOut();

            }

        }
     
        public AuthenticationState AuthenticatedState(string userId = "userId",string name="UserName", string role="User")
        {
            

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role)
            }, "Cookies");

            var user = new ClaimsPrincipal(identity);

            return  new AuthenticationState(user);
        }
        public void MarkUserAsAuthenticated(string token)
        {
            var auth= AuthenticatedState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(auth));

        }

 
        public void MarkUserAsJWTAuthenticated(string token)
        {
            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "Bearer");
            var user = new ClaimsPrincipal(identity);

            var authenticatedState = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(authenticatedState));
        }
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = WebEncoders.Base64UrlDecode(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            NotifyAuthenticationStateChanged(Task.FromResult(anonymousState));
        }
    }

}
