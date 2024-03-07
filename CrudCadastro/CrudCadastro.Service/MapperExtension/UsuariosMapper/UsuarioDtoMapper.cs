using CrudCadastro.Common.Dtos.Usuarios;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;

namespace CrudCadastro.Service.MapperExtension.UsuariosMapper;

public static class UsuarioDtoMapper
{
    public static UsuarioDto MapToUsuarioDto (Usuario src)
    {
        return new UsuarioDto
        {   
            Id = src.Id,
            Nome = src.Nome,
            Email = src.Email
        };
    }

    public static List<UsuarioDto> MapToUsuarioDto(ICollection<Usuario> src)
    {
       return src.Select(x => MapToUsuarioDto(x)).ToList();
    }
}
