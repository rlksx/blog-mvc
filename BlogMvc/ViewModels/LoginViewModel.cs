using System.ComponentModel.DataAnnotations;

namespace BlogMvc.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Informe o e-mail!")]
    [EmailAddress(ErrorMessage = "e-mail invalido!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Informe a senha correta!")]
    public string Password { get; set; }
}