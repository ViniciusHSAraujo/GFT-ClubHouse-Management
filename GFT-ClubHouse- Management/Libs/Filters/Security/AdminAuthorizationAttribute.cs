using System;
using GFT_ClubHouse__Management.Libs.Login;
using GFT_ClubHouse__Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GFT_ClubHouse__Management.Libs.Filters.Security {
    public class AdminAuthorizationAttribute : Attribute, IAuthorizationFilter {
        private LoginAdmin _loginAdmin;

        public void OnAuthorization(AuthorizationFilterContext context) {
            _loginAdmin = context.HttpContext.RequestServices.GetService(typeof(LoginAdmin)) as LoginAdmin;
            var user = _loginAdmin.GetUser();

            if (user == null)
                context.Result = new RedirectToActionResult("login", "home", new {Area = "Administrator"});
        }
    }
}