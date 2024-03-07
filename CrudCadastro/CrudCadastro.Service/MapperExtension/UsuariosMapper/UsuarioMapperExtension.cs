using CrudCadastro.Common.Dtos.Usuarios;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;

namespace CrudCadastro.Service.MapperExtension.UsuariosMapper;

public static class UsuarioMapperExtension
{
    public static T MapTo<T>(this Usuario src)
    {
        var usuario = new List<Usuario>{src};

        return usuario.MapTo<IList<T>>().First();
    }

    public static T MapTo<T>(this ICollection<Usuario> src)
    {
        var interfaceOfGeneric = typeof(T).GetInterfaces();
        
        if(interfaceOfGeneric.Any(x => x == typeof(ICollection<UsuarioDto>)))
        {
            return (T)(object)UsuarioDtoMapper.MapToUsuarioDto(src);
        }

        throw new Exception("Not Implemented Interface");
    }

    public static Usuario MapToNew(this UsuarioInsertDto dto) 
    {
        return new Usuario()
        {
            Nome = dto.Nome,
            Email = dto.Email,
        };
    }
}
