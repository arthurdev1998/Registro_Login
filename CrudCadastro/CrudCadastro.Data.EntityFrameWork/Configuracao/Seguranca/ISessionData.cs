using CrudCadastro.Data.EntityFrameWork.Configuracao.Seguranca;

namespace CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;

public interface ISessionData
{
    public UserSession? userSession { get; set; }
}