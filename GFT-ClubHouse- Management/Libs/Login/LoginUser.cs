using System;
using GFT_ClubHouse__Management.Libs.Sessions;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.Enum;
using Newtonsoft.Json;

namespace GFT_ClubHouse__Management.Libs.Login {
    public class LoginUser {
        private readonly Session _session;
        private readonly string key = "Login.Admin";

        public LoginUser(Session session) {
            _session = session;
        }

        public void Login(User user) {
            if (user.Roles == UserRoles.User) {
                var userJson = JsonConvert.SerializeObject(user);
                _session.Create(key, userJson);
            }
            else {
                throw new Exception("Invalid login.");
            }
        }

        public User GetUser() {
            var userJson = _session.Get(key);
            if (userJson == null) return null;
            User user = JsonConvert.DeserializeObject<User>(userJson);
            return user;
        }

        public void Logout() {
            _session.Clear();
        }
    }
}