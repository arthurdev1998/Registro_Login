namespace CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;

public class Usuario 
{
    public int Id { get; set; }
    public required string? Nome { get; set; }
    public required string? Email { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
}