﻿using Metrik.Shared.Entities.Abstract;

namespace Metrik.Entities.Dtos
{
    public class UserLoginDto:DtoGetBase
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; } 
    }
}
