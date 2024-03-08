namespace CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;

public interface IUsuarioRepository
{
    public Task<List<Usuario>> GetAllAsync(bool asNoTracking);
    public Task<Usuario> GetByIdAsync(int id);
    public Task<Usuario> GetUsuarioByEmail(string email);
    public Task<Usuario> AdicionarUsuario(Usuario usuario);
    public Task Update(Usuario usuario);
    public Task Remove(Usuario usuario); 
    public Task<bool> AuthenticateAsync(string? email, string? senha);
    public string GenerateToken(int id, string email);
    public Task<bool> UserExist(string? email);
}
