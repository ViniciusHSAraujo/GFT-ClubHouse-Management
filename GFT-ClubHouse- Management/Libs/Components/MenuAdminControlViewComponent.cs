using System.Threading.Tasks;
using GFT_ClubHouse__Management.Libs.Login;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Libs.Components {
    public class MenuAdminControlViewComponent: ViewComponent {
        
        private readonly LoginAdmin _loginAdmin;
        
        public MenuAdminControlViewComponent(LoginAdmin loginAdmin) {
            _loginAdmin = loginAdmin;
        }
        
        public async Task<IViewComponentResult> InvokeAsync() {
            var user = _loginAdmin.GetUser();
            return View(user);
        }
    }
}