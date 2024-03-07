using CrudCadastro.Common.Dtos.Usuarios;
using CrudCadastro.Service.Services.UsuarioService;
using Microsoft.AspNetCore.Mvc;

namespace CrudCadastro.Api.Controllers;

[ApiController]
[Route("api")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioInsertHandler _usuarioInsertHandler;

    public UsuarioController(UsuarioInsertHandler usuarioInsertHandler)
    {
        _usuarioInsertHandler = usuarioInsertHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(UsuarioInsertDto usuario)
    {
        if(usuario == null)
            return BadRequest("usuario nao pode ser nulo");
        
        var usuariocadastrado = await _usuarioInsertHandler.ExecuteAsync(usuario);

        return Ok(usuariocadastrado);
    }
}
