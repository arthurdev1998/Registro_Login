using System.Security.Claims;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Seguranca;
using CrudCadastro.Data.EntityFrameWork.Configuracao.Usuarios;

namespace CrudCadastro.Api.Security.Middlewares;

public class SessionDataMeddleware
{
    private readonly RequestDelegate _next;
    private readonly ISessionData _sessionData;

    public SessionDataMeddleware(RequestDelegate next, ISessionData sessionData)
    {
        _next = next;
        _sessionData = sessionData;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
        {
            var userSession = new UserSession
            {
                Id = Convert.ToInt32(context.User.FindFirstValue("Id")),
                Email = context.User.FindFirstValue("Email")
            };

            _sessionData.userSession = userSession;
            //Nao se esqueça que vc pode usar o context.Items (dicionaro key value) para 
            // pegar os valores do id e do email
            // utilizando antes disso         
            //int id = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));

            context.Items["UserSession"] = userSession;
        }
        await _next(context);
    }
}