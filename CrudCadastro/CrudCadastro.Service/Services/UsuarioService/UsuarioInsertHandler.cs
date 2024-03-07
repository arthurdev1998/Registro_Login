using System.Security.Cryptography;
using System.Text;
using CrudCadastro.Common.Dtos.Usuarios;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;
using CrudCadastro.Service.MapperExtension.UsuariosMapper;
using Npgsql.Replication;

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
        var entity = usuario.MapToNew();
        
        if(usuario.Password == null)
            throw new Exception("usuario nao pode ser nulo");
    
            using var hmac = new HMACSHA512();
            var bytes = Encoding.UTF8.GetBytes(usuario.Password);
            byte[] passwordHash = hmac.ComputeHash(bytes);
            byte[] passwordSalt = hmac.Key;

            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;

            var registro = await _usuarioRepository.AdicionarUsuario(entity);
            return registro.MapTo<UsuarioDto>();
    }
}