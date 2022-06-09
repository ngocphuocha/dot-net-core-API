using System;

namespace TodoApp.Configration
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public TimeSpan ExpiryTimeFrame { get; internal set; }
    }
}
