using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GFT_ClubHouse__Management.Libs.ExtensionsMethods {
    public static class ModelStateExtension {
        public static List<string> ListErrors(this ModelStateDictionary modelstate) {
            return modelstate
                .Values.SelectMany(v => v.Errors)
                .Select(v => v.ErrorMessage + " " + v.Exception).ToList();
        }
    }
}