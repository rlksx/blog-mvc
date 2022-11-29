using BlogMvc.Data;
using BlogMvc.Extensions;
using BlogMvc.Services;
using BlogMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogMvc.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    // private readonly TokenService _tokenService;
    // public AccountController(TokenService tokenService)
        // => _tokenService = tokenService;
        
    [HttpPost("v1/accounts/")]
    public async Task<IActionResult> Post(
        [FromBody] RegisterViewModel model,
        [FromServices] BlogDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
    }
    
    [HttpPost("v1/account/login/")]
    public IActionResult Login([FromServices] TokenService tokenService)
    {
        /* gerando novo token */
        var token = tokenService.GenerateToken(null);

        return Ok(token);
    }
    
    
}