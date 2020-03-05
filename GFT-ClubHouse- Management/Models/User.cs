using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFT_ClubHouse__Management.Libs.Language;
using GFT_ClubHouse__Management.Models.Enum;

namespace GFT_ClubHouse__Management.Models {
    public class User {
        public int Id { get; set; }

        [Display(Name = "First Name")] public string Name { get; set; }

        [Display(Name = "Last Name")] public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [Phone]
        public string Phone { get; set; }

        [Display(Name = "Address")]
        [ForeignKey("Address")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public int AddressId { get; set; }

        [ForeignKey("AddressId")] public virtual Address Address { get; set; }

        [Display(Name = "E-Mail")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(6, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(64, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E003")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E005")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Roles")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public UserRoles Roles { get; set; }
    }
}