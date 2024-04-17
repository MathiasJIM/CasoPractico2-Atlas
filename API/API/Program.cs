using Abstracciones.BC;
using Abstracciones.BW;
using Abstracciones.Modelos;
using BC;
using BW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutorizacionMiddleware;
using Abstracciones.DA;
using DA;
using DA.Repositorio;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Configuracion Token

            var tokenConfiguracion = builder.Configuration.GetSection("Token").Get<TokenConfiguracion>();
            var jwtIssuer = tokenConfiguracion.Issuer;
            var jwtKey = tokenConfiguracion.Key;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtIssuer,
                     ValidAudience = jwtIssuer,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                 };
             });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();
            builder.Services.AddScoped<ISeguridadDA, SeguridadDA>();
            builder.Services.AddScoped<IAutenticacionBW, AutenticacionBW>();
            builder.Services.AddScoped<IAutenticacionBC, AutenticacionBC>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            //Autorización y perfiles
            app.UseMiddleware<ClaimsPerfilesMiddleware>();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
