using CrudCadastro.Common.Dtos.Usuarios;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Paginacoes;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;
using CrudCadastro.Service.MapperExtension.UsuariosMapper;

namespace CrudCadastro.Service.Services.UsuarioService;

public class UsuarioGetAllHandler
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioGetAllHandler(IUsuarioRepository usuarioRepository)
    {
       _usuarioRepository = usuarioRepository; 
    }

    public async Task<PageList<UsuarioDto>> ExecuteAsync( int pageNumber, int itensByPage, bool asNoTracking = false)
    {
        var registros = await _usuarioRepository.GetAllAsync(pageNumber, itensByPage,asNoTracking);
        
        var mapeamento  = registros.MapTo<List<UsuarioDto>>();

         return new PageList<UsuarioDto>(mapeamento, pageNumber, itensByPage, registros.Count);
    }
}