using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFT_ClubHouse__Management.Libs.Language;

namespace GFT_ClubHouse__Management.Models.ViewModels.API.ClubHouseViewModel {
    public class ClubHouseCreateViewModel {
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(80, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E003")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(80, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E003")]
        public string Street { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(80, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E003")]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E002")]
        public string Zip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(12, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E003")]
        public string State { get; set; }
    }
}