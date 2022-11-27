using BlogMvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogMvc.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    /* API REST - endpoints com o mesmo nome do controller no plural em minusculo */
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context) 
        => Ok(await context.Categories.ToListAsync());
    /* await - aguarda o método retornar para continuar a executar async */
    
    /* async - indica que as ações serão executadas assicronamente,
     e só retornam Taks<T> */
    
    // [HtppGet("v2/categories")] /=> versionamento;
    // public IActionResult Getv2([FromServices] BlogDataContext context) 
    //     => Ok(context.Categories.ToList());
}