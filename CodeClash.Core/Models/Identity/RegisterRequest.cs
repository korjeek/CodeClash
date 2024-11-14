﻿using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Models.Identity;

public class RegisterRequest
{
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; } = null!;
    
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = null!;
    
    [Required]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    public string PasswordConfirm { get; set; } = null!;
}