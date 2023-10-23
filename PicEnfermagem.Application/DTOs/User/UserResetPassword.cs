using System.ComponentModel.DataAnnotations;

namespace PicEnfermagem.Application.DTOs.User;

public sealed class UserResetPassword
{
    [Required(ErrorMessage = "A senha deve ser informada")]
    public string Password { get; set; }
    [Compare(nameof(Password), ErrorMessage = "As senhas devem ser iguais.")]
    public string ConfirmPassword { get; set; }    
    public string Token { get; set; }    
}
