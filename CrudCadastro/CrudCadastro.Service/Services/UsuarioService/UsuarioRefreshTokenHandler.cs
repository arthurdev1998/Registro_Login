using System.Net.Http;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;

namespace CrudCadastro.Service.Services.UsuarioService;

public class UsuarioRefreshTokenHandler
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioRefreshTokenHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public Task<UsuarioToken> Execute(int id, string email)
    {
   
        var token = _usuarioRepository.GenerateToken(id, email);

        return Task.FromResult(new UsuarioToken
        {
            Token = token
        });
    }
}
