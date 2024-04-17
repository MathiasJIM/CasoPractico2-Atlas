using Abstracciones.DA;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Abstracciones.Modelos;
using System.Security.Claims;

namespace AutorizacionMiddleware
{
    public class ClaimsPerfilesMiddleware
    {
        private readonly RequestDelegate _next;

        public ClaimsPerfilesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ISeguridadDA seguridadDA)
        {
            var claims = new List<Claim>();

            if (httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
            {
                await ObtenerUsuario(httpContext, seguridadDA, claims);
            }

            var appIdentity = new ClaimsIdentity(claims);
            httpContext.User.AddIdentity(appIdentity);
            await _next(httpContext);
        }

        private async Task ObtenerUsuario(HttpContext httpContext, ISeguridadDA seguridadDA, List<Claim> claims)
        {
            var usuarioClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "correo");
            if (usuarioClaim != null)
            {
                var usuario = await seguridadDA.ObtenerUsuario(new Login() { Email = usuarioClaim.Value });
                claims.Add(new Claim(ClaimTypes.Email, usuario.Email));
                claims.Add(new Claim("IdUsuario", usuario.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, usuario.IdRol.ToString()));
            }
        }
    }

    public static class ClaimsUsuarioMiddlewareExtensions
    {
        public static IApplicationBuilder UseAutorizacionClaims(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClaimsPerfilesMiddleware>();
        }
    }
}
