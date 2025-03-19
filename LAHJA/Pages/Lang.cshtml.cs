using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LAHJA.Pages
{
    public class LangModel : PageModel
    {
        public void OnGet()
        {
            string? culture = Request.Query["culture"];
            Console.WriteLine("new selected language: " + culture);
            if (culture != null)
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }


            string returnUrl = Request.Headers["Referer"].ToString() ?? "/";
            Response.Redirect(returnUrl);
        }

        //public void SetTokenInCookie(string token)
        //{
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        Response.Cookies.Append(
        //            ConstantsApp.ACCESS_TOKEN,  // «”„ «·ﬂÊﬂÌ
        //            token,        // «· Êﬂ‰
        //            new CookieOptions
        //            {
        //                HttpOnly = true,  // Ã⁄· «·ﬂÊﬂÌ“ €Ì— ﬁ«»·… ··Ê’Ê· ⁄»— JavaScript
        //                Secure = true,    // ≈—”«· «·ﬂÊﬂÌ“ ›ﬁÿ ⁄»— HTTPS
        //                Expires = DateTimeOffset.UtcNow.AddHours(1),  // „œ… ’·«ÕÌ… «·ﬂÊﬂÌ
        //                SameSite = SameSiteMode.Lax,  // Õ„«Ì… ÷œ ÂÃ„«  CSRF
                        
        //            }
        //        );
        //    }
        //}

        //public string GetTokenFromCookie()
        //{
        //    return Request.Cookies[ConstantsApp.ACCESS_TOKEN];
        //}

        //// ÿ—Ìﬁ… ·Õ–› «· Êﬂ‰ „‰ «·ﬂÊﬂÌ
        //public void RemoveTokenFromCookie()
        //{
        //    Response.Cookies.Delete(ConstantsApp.ACCESS_TOKEN);
        //}

    }
}
