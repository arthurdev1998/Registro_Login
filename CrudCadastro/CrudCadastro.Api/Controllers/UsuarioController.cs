using CrudCadastro.Common.Dtos.Usuarios;
using CrudCadastro.Service.Services.UsuarioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudCadastro.Api.Controllers;

[ApiController]
[Route("api")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioInsertHandler _usuarioInsertHandler;
    private readonly UsuarioLoginHandler _usuarioLoginHandler;

    public UsuarioController(UsuarioInsertHandler usuarioInsertHandler,
                                UsuarioLoginHandler usuarioLoginHandler)
    {
        _usuarioInsertHandler = usuarioInsertHandler;
        _usuarioLoginHandler = usuarioLoginHandler;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Registrar(UsuarioInsertDto usuario)
    {
        if(usuario == null)
            return BadRequest("usuario nao pode ser nulo");
        
        var usuariocadastrado = await _usuarioInsertHandler.ExecuteAsync(usuario);

        return Ok(usuariocadastrado);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UsuarioInsertDto usuario)
    {
        if(usuario == null)
            return BadRequest("usuario nao pode ser nulo");
        
        var loginUsuario = await _usuarioLoginHandler.ExecuteAsync(usuario);

        if(loginUsuario != null)
            return Ok(loginUsuario);
        
        return Unauthorized("usuario nao autorizado");
    }   
}