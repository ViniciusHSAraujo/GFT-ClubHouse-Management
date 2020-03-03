using System.ComponentModel.DataAnnotations;
using GFT_ClubHouse__Management.Libs.Language;

namespace GFT_ClubHouse__Management.Models.ViewModels.API.MusicalGenreViewModels {
    public class MusicalGenreEditViewModel {
        
        
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public int Id { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(80, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E003")]
        public string Name { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public bool IsActive { get; set; }
    }
}