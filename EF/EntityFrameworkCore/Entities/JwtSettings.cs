﻿namespace EntityFrameworkCore.Entities
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
