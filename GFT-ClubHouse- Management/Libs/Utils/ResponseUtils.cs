using GFT_ClubHouse__Management.Models.ViewModels.API;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Libs.Utils {
    public static class ResponseUtils {
        public static ObjectResult GenerateObjectResult(string message, object data = null){
            return new ObjectResult(new ResultViewModel<object>(message, data));
        }
    }
}