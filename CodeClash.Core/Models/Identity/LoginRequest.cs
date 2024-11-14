using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Models.Identity;

public class LoginRequest(string email, string password)
{
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; } = email;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = password;
}