using BlogMvc.Data;
using BlogMvc.Extensions;
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
    {
        try
        {
            var category = await context.Categories.ToListAsync();
            // retorna todas as categorias
            return Ok(new ResultViewModel<List<Category>>(category));
        }
        catch (Exception exception)
        {
            return StatusCode(500, new ResultViewModel<List<Category>>("Falha interna so servidor"));
        }
    }
    /* await - aguarda o método retornar para continuar a executar async */

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null) return NotFound(
            new ResultViewModel<Category>("14TY3 - Categoria não encontrada!")); // ERROR 404
        
        return Ok(new ResultViewModel<Category>(category));
    }


    [HttpPost("v1/categories/")]
    public async Task<IActionResult> PostAsync(
        [FromServices] BlogDataContext context,
        [FromBody] EditorCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            // return BadRequest(ModelState.Values);
            return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
        
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

            return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException exception)
        {
            return StatusCode(500, new ResultViewModel<Category>("Não foi possivel incluir a categoria :("));
        }
        catch (Exception exception)
        {
            return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
        }
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromServices] BlogDataContext context,
        [FromBody] EditorCategoryViewModel model,
        [FromRoute] int id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado")); // ERROR 404
        
        category.Name = model.Name;
        category.Slug = model.Slug;

        try
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
            
            return Ok(new ResultViewModel<Category>(category));
        }
        catch (Exception exception)
        {
            return StatusCode(500, new ResultViewModel<Category>(category));
        }
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] BlogDataContext context,
        [FromRoute] int id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado")); // 404

        try
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Category>(category));    
        }
        catch (Exception exception)
        {
            return StatusCode(500, new ResultViewModel<Category>("Não foi possivel Rxcluir essa categoria!"));
        }
    }
    
    /* async - indica que as ações serão executadas assicronamente,
     e só retornam Taks<T>
    [HtppGet("v2/categories")] /=> versionamento;
    public IActionResult Getv2([FromServices] BlogDataContext context) 
        => Ok(context.Categories.ToList()); */
    
    /* await usando quando é o banco é acessado */
}