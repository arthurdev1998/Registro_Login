using CrudCadastro.Common.Dtos.Usuarios;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;

namespace CrudCadastro.Service.Services.UsuarioService;

public class UsuarioInsertHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioInsertHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;        
    }

    public async Task<UsuarioDto> ExecuteAsync(UsuarioInsertDto usuario)
    {
        
    }
}
