using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace API.Services.Jwtservices
{
    public class JwtAuthenticationEvents : JwtBearerEvents
    {
        public override Task MessageReceived(MessageReceivedContext context)
        {
            context.Token = context.Request.Cookies["access_token"];
            return Task.CompletedTask;
        }
    }
}
