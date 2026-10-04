using System;

namespace API.Dtos.ResponseDtos
{
    public class LoginResponseDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        public LoginResponseDto(string username,string email,string role,string token) 
        {
            Username = username;
            Email = email;
            Role = role;
            Token = token;
        }
    }
}
