# nullable disable
using CrudCadastro.Common.Dtos.Usuarios;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;

namespace CrudCadastro.Service.Services.UsuarioService;

public class UsuarioLoginHandler
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioLoginHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioToken> ExecuteAsync(UsuarioInsertDto usuario)
    {
        var userExiste = await _usuarioRepository.UserExist(usuario.Email);
        if(!userExiste)
            return null;
        
        var userAuthenticate = await _usuarioRepository.AuthenticateAsync(usuario.Email, usuario.Password);
        if(!userAuthenticate)
            return null;

        var usuarioEntity = await _usuarioRepository.GetUsuarioByEmail(usuario.Email);
        if(usuario == default)
            return null;

        var token = _usuarioRepository.GenerateToken(usuarioEntity!.Id, usuarioEntity.Email!);

        return new UsuarioToken
        {
            Token = token
        };
    }
}