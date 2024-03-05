using CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace CrudCadastro.Data.EntityFrameWork.Data;

public class AppDbContext : DbContext
{
      public AppDbContext() : base()
    {}
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Usuario> Usuario { get; set; }
}
