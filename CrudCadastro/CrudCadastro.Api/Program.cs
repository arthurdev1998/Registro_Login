using System.Text;
using CrudCadastro.Api.Security.Middlewares;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Seguranca;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;
using CrudCadastro.Data.EntityFrameWork.Data;
using CrudCadastro.Service.Services.UsuarioService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.EnableAnnotations();
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Omae wa mo shindei ru "
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
       {
         new OpenApiSecurityScheme()
         {
            Reference = new OpenApiReference()
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer",
            }
         },
         new string[] {}
       }
    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {
       options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
       {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true, // tempo que o token espirará
            ValidateIssuerSigningKey = true,

            ValidIssuer = configuration["jwt:issuer"],
            ValidAudience = configuration["jwt:audience"],
           
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:secretKey"]!)), ClockSkew = TimeSpan.Zero
       }; 
    });

//Inicio Bloco Injecao de dependencia
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CONNECTION") ?? throw new InvalidOperationException("Connection string 'ProjetoMVCContext' not found.")));


builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioInsertHandler>();
builder.Services.AddScoped<UsuarioLoginHandler>();
builder.Services.AddScoped<UsuarioGetAllHandler>();
builder.Services.AddSingleton<ISessionData, SessionData>();

//#Fim do bloco Injecao de dependencia

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<SessionDataMeddleware>();

app.Run();