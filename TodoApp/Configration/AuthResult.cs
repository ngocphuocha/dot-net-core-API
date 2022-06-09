using System.Collections.Generic;

namespace TodoApp.Configration
{
    public class AuthResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Result { get; set; }
        public List<string> Errors { get; set; }
        public bool Success { get; internal set; }
    }
}
