#nullable disable
using System.IdentityModel.Tokens.Jwt;
using System.IO.Compression;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Paginacoes;
using CrudCadastro.Data.EntityFrameWork.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public UsuarioRepository(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<Usuario> AdicionarUsuario(Usuario usuario)
    {
        var usuarioIsExist = UserExist(usuario.Email);

        if (usuarioIsExist.Result)
            throw new Exception("Usuario Já existe");

        await _context.AddAsync(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task<bool> AuthenticateAsync(string email, string senha)
    {
        if (email == default || senha == default)
            return false;

        var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Email != null && x.Email.ToLower() == email.ToLower());

        if (usuario == default || usuario.PasswordHash == default)
            return false;

        using var hmac = new HMACSHA512(usuario.PasswordSalt!);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));

        if (!Enumerable.SequenceEqual(usuario.PasswordHash, computedHash))
            return false;

        return true;
    }

    public string GenerateToken(int id, string email)
    {
        var claims = new[]
        {
            new Claim("Id", id.ToString()),
            new Claim("Email", email.ToLower()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretkey"]!));

        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddMinutes(10);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["jwt:issuer"],
            audience: _configuration["jwt:audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<PageList<Usuario>> GetAllAsync(int pageNumber, int itensByPage, bool asNoTracking)
    {
        var query = _context.Usuario.AsQueryable();

        if (asNoTracking)
            query.AsNoTracking();

        return await PaginationExtension.CreatePagenationAsync(query, pageNumber, itensByPage);
    }

    public async Task<Usuario> GetUsuarioByEmail(string email)
    {
        return await _context.Usuario.SingleOrDefaultAsync(x => x.Email != null && x.Email.ToLower() == email.ToLower());
    }

    public async Task<Usuario> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task Remove(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public Task Update(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UserExist(string email)
    {
        if (email == null)
            return false;

        return await _context.Usuario.AnyAsync(x => x.Email != default && x.Email.ToLower() == email.ToLower());
    }
}
