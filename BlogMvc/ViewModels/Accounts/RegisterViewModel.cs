using System.ComponentModel.DataAnnotations;

namespace BlogMvc.ViewModels.Accounts;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Este campo name é obrigatório!")]
    public string Name { get; set; }
    [Required(ErrorMessage = "O campo email é obrigatório!")]
    public string Email { get; set; }
}