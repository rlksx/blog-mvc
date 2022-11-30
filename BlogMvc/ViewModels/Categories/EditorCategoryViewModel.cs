using System.ComponentModel.DataAnnotations;

namespace BlogMvc.ViewModels.Categories;

public class EditorCategoryViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório!")]
    [StringLength(40, MinimumLength = 2, ErrorMessage = "O campo deve conter entre 40 e 2 caracteres!")]
    public string Name { get; set; }
    [Required(ErrorMessage = "O slug é obrigatório!")]
    public string Slug { get; set; }
}