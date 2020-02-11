using Microsoft.AspNetCore.Http;

namespace GFT_ClubHouse__Management.Libs.Sessions {
    public class Session {
        private readonly IHttpContextAccessor _context;

        public Session(IHttpContextAccessor context) {
            _context = context;
        }

        public void Create(string key, string value) {
            _context.HttpContext.Session.SetString(key, value);
        }

        public void Update(string key, string value) {
            Remove(key);
            _context.HttpContext.Session.SetString(key, value);
        }

        public void Remove(string key) {
            if (Exists(key)) {
                _context.HttpContext.Session.Remove(key);
            }
        }

        public void Clear() {
            _context.HttpContext.Session.Clear();
        }

        public string Get(string key) {
            return _context.HttpContext.Session.GetString(key);
        }

        public bool Exists(string key) {
            return _context.HttpContext.Session.TryGetValue(key, out byte[] value);
        }
    }
}