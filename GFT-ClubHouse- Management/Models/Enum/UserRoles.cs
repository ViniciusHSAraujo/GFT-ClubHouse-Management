using System.ComponentModel.DataAnnotations;

namespace GFT_ClubHouse__Management.Models.Enum {
    public enum UserRoles {
        [Display(Name = "Administrator")] Admin,
        [Display(Name = "User")] User
    }
}