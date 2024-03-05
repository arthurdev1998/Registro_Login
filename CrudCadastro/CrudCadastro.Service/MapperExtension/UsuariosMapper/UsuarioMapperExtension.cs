using CrudCadastro.Common.Dtos.Usuarios;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;

namespace CrudCadastro.Service.MapperExtension.UsuariosMapper;

public static class UsuarioMapperExtension
{
    public static T MapTo<T>(this Usuario src)
    {
        var usuario = new List<Usuario>{src};
        return usuario.MapTo<ICollection<T>>().First();
    }

    public static T MapTo<T>(this ICollection<Usuario> src)
    {
        var interfaceOfGeneric = typeof(T).GetInterfaces();
        if(interfaceOfGeneric.Any(x => x == typeof(UsuarioDto)))
        {
            return (T)(object)UsuarioDtoMapper.MapToUsuarioDto(src);
        }

        throw new Exception("Not Implemented Interface");
    }
}
