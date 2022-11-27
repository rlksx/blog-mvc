using BlogMvc.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlogMvc.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    /* API REST - endpoints com o mesmo nome do controller no plural em minusculo */
    [HttpGet("categories")]
    public IActionResult Get([FromServices] BlogDataContext context) 
        => Ok(context.Categories.ToList());
}