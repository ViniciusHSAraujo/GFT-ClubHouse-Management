using GFT_ClubHouse__Management.Libs.Filters.Security;
using GFT_ClubHouse__Management.Libs.Login;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Controllers {
    [UserAuthorization]
    public class OrdersController : Controller {
        private readonly ISaleRepository _saleRepository;
        private readonly LoginUser _loginUser;
        
        public OrdersController(ISaleRepository saleRepository, LoginUser loginUser) {
            _saleRepository = saleRepository;
            _loginUser = loginUser;
        }

        public IActionResult Index() {
            var user = _loginUser.GetUser();
            var sales = _saleRepository.GetByUser(user.Id);
            return View(sales);
        }
        
        public IActionResult Details(int id) {
            var user = _loginUser.GetUser();
            var sale = _saleRepository.GetById(id);
            if (sale.UserId != user.Id) {
                return new UnauthorizedResult();
            }
            return View(sale);
        }
        
    }
}