using System.ComponentModel.DataAnnotations;

namespace PicEnfermagem.Application.DTOs.User;

public class UserInsertRequest
{
    [Required(ErrorMessage = "O código de aluno deve ser informado.")]
    public string Username { get; set; }
    [Required(ErrorMessage = "A senha deve ser informada.")]
    public string Password { get; set; }
    [Compare(nameof(Password), ErrorMessage = "As senhas devem ser iguais.")]
    public string ConfirmPassword { get; set; }
}
