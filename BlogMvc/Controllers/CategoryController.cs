using BlogMvc.Data;
using BlogMvc.Models;
using BlogMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogMvc.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    /* API REST - endpoints com o mesmo nome do controller no plural em minusculo */
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context) 
        => Ok(await context.Categories.ToListAsync()); // retorn todas as categorias
    /* await - aguarda o método retornar para continuar a executar async */

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null) return NotFound(); // ERROR 404
        
        return Ok(category);
    }


    [HttpPost("v1/categories/")]
    public async Task<IActionResult> PostAsync(
        [FromServices] BlogDataContext context,
        [FromBody] CreateCategotyViewModel model)
    {
        try
        {
            var category = new Category
            {
                Id = 0,
                Name = model.Name,
                Slug = model.Slug.ToLower()
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return Created($"v1/categories/{category.Id}", category);
        }
        catch (DbUpdateException exception)
        {
            return StatusCode(500, "Não foi possivel incluir a categoria :(");
        }
        catch (Exception exception)
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromServices] BlogDataContext context,
        [FromBody] Category model,
        [FromRoute] int id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            return NotFound(); // ERROR 404
        
        category.Name = model.Name;
        category.Slug = model.Slug;

        try
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
            
            return Ok();
        }
        catch (Exception exception)
        {
            return StatusCode(500, "Falha interna no servidor, não foi possivel alterar a categoria!");
        }
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] BlogDataContext context,
        [FromRoute] int id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            return NotFound(); // 404

        try
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Ok(category);    
        }
        catch (Exception exception)
        {
            return StatusCode(500, "Não foi possivel deletar a categoria!");
        }
        
    }
    
    /* async - indica que as ações serão executadas assicronamente,
     e só retornam Taks<T>
    [HtppGet("v2/categories")] /=> versionamento;
    public IActionResult Getv2([FromServices] BlogDataContext context) 
        => Ok(context.Categories.ToList()); */
    
    /* await usando quando é o banco é acessado */
}