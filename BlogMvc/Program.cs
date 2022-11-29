using System.Text;
using BlogMvc;
using BlogMvc.Data;
using BlogMvc.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = false);

builder.Services.AddDbContext<BlogDataContext>();
/* serviços de dependencia do token */
builder.Services.AddTransient<TokenService>(); //=> cria a cade instância um novo token;
// builder.Services.AddScoped(); //=> utiliza um unico token para cada transação;
// builder.Services.AddSingleton(); //=> utiliza o mesmo objeto até que a aplicação pare;

var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
builder.Services.AddAuthentication(x =>
{
    /* padrão de autentificação, diz como é a autentificação */
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
        
    };
});

var app = builder.Build();
/* validaçaõ, indentificação e autorização */
app.UseAuthentication();
app.UseAuthorization();

/* mapeando controler */
app.MapControllers();

app.Run();
