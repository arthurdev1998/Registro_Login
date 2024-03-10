using CrudCadastro.Common.Dtos.Usuarios;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;
using CrudCadastro.Service.Services.UsuarioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CrudCadastro.Api.Controllers;

[ApiController]
[Route("api")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioInsertHandler _usuarioInsertHandler;
    private readonly UsuarioLoginHandler _usuarioLoginHandler;
    private readonly UsuarioGetAllHandler _usuarioGetAllHandler;

    public UsuarioController(UsuarioInsertHandler usuarioInsertHandler,
                                UsuarioLoginHandler usuarioLoginHandler,
                                UsuarioGetAllHandler usuarioGetAllHandler)
    {
        _usuarioInsertHandler = usuarioInsertHandler;
        _usuarioLoginHandler = usuarioLoginHandler;
        _usuarioGetAllHandler = usuarioGetAllHandler;
    }

    [SwaggerOperation(Summary = "Inseri um Usuario")]
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(UsuarioDto), 200)]
    public async Task<IActionResult> Registrar(UsuarioInsertDto usuario)
    {
        if (usuario == null)
            return BadRequest("usuario nao pode ser nulo");

        var usuariocadastrado = await _usuarioInsertHandler.ExecuteAsync(usuario);

        return Ok(usuariocadastrado);
    }

    [SwaggerOperation(Summary = "Retorna um Token ao logar com um usuario")]
    [HttpPost("login")]
    [ProducesResponseType(typeof(UsuarioToken), 200)]
    public async Task<IActionResult> Login(UsuarioInsertDto usuario)
    {
        if (usuario == null)
            return BadRequest("usuario nao pode ser nulo");

        var loginUsuario = await _usuarioLoginHandler.ExecuteAsync(usuario);

        if (loginUsuario != null)
            return Ok(loginUsuario);

        return Unauthorized("usuario nao autorizado");
    }

    [SwaggerOperation(Summary = "Retorna uma Lista de Usuarios")]
    [HttpGet]
    [ProducesResponseType(typeof(List<UsuarioDto>), 200)]
    public async Task<IActionResult> GetAllUser([FromQuery] bool asnotracking, int pageNumber, int itensByPage)
    {
        var registro = await _usuarioGetAllHandler.ExecuteAsync(pageNumber, itensByPage, asnotracking);
        return Ok(registro.Data);
    }
}