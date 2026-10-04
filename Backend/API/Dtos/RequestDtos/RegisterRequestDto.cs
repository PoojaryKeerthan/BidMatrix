using System.ComponentModel.DataAnnotations;

namespace API.Dtos.RequestDtos
{
    public class RegisterRequestDto
    {
        [Required]
        [StringLength(50,MinimumLength =3)]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = null!;
    }
}
