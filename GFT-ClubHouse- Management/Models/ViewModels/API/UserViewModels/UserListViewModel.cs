namespace GFT_ClubHouse__Management.Models.ViewModels.API.UserViewModels {
    public class UserListViewModel {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public Address Address { get; set; }

        public string Email { get; set; }

        public string Roles { get; set; }
    }
}