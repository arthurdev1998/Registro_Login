using CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;

namespace CrudCadastro.Data.EntityFrameWork.Configuracao.Seguranca;

public class SessionData : ISessionData
{
    public UserSession? userSession { get; set; }
}
