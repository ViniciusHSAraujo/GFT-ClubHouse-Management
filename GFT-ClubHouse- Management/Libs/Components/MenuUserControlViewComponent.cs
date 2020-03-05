using System.Threading.Tasks;
using GFT_ClubHouse__Management.Libs.Login;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Libs.Components {
    public class MenuUserControlViewComponent : ViewComponent {
        private readonly LoginUser _loginUser;

        public MenuUserControlViewComponent(LoginUser loginUser) {
            _loginUser = loginUser;
        }

        public IViewComponentResult Invoke() {
            var user = _loginUser.GetUser();
            return View(user);
        }
    }
}