using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;

public class Usuario
{
    [Key, Column("cod_serial_usuario")]
    public int Id { get; set; }

    [Column("nome")]
    public required string? Nome { get; set; }

    [Column("email")]
    public required string? Email { get; set; }

    [Column("passwordhash")]
    public byte[]? PasswordHash { get; set; }

    [Column("passwordsalt")]
    public byte[]? PasswordSalt { get; set; }
}