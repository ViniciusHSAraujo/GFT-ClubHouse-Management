using System;
using GFT_ClubHouse__Management.Libs.Login;
using GFT_ClubHouse__Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GFT_ClubHouse__Management.Libs.Filters.Security {
    public class UserAuthorizationAttribute: Attribute, IAuthorizationFilter {
        
        private LoginUser _loginUser;

        public void OnAuthorization(AuthorizationFilterContext context) {
            _loginUser = context.HttpContext.RequestServices.GetService(typeof(LoginUser)) as LoginUser;
            User user = _loginUser.GetUser();
            if (user == null) {
                context.Result = new RedirectToActionResult("login", "Home", new{ Area = "Users"});
            }
        }
    }
}