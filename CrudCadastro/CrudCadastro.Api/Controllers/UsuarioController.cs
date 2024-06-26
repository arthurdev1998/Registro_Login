﻿using System.Security.Claims;
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
    private readonly UsuarioRefreshTokenHandler _usuarioRefreshTokenHandler;

    public UsuarioController(UsuarioInsertHandler usuarioInsertHandler,
                                UsuarioLoginHandler usuarioLoginHandler,
                                UsuarioGetAllHandler usuarioGetAllHandler,
                                UsuarioRefreshTokenHandler usuarioRefreshTokenHandler)
    {
        _usuarioInsertHandler = usuarioInsertHandler;
        _usuarioLoginHandler = usuarioLoginHandler;
        _usuarioGetAllHandler = usuarioGetAllHandler;
        _usuarioRefreshTokenHandler = usuarioRefreshTokenHandler;
    }

    [SwaggerOperation(Summary = "Insere um Usuario")]
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
    [Authorize]
    [ProducesResponseType(typeof(List<UsuarioDto>), 200)]
    public async Task<IActionResult> GetAllUser([FromQuery] bool asnotracking, int pageNumber, int itensByPage)
    {
        var registro = await _usuarioGetAllHandler.ExecuteAsync(pageNumber, itensByPage, asnotracking);
        return Ok(registro.Data);
    }

    [HttpGet("RefreshToken")]
    [Authorize]
    [ProducesResponseType(typeof(List<UsuarioDto>), 200)]
    public UsuarioToken RefreshToken()
    {

        int id = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));
        var email = HttpContext.User.FindFirstValue("Email");

        var token = _usuarioRefreshTokenHandler.Execute(id, email!);
        
        return token.Result;
    }
}