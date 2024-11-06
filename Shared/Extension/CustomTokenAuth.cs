using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared;

namespace Shared.Extension
{
    public static class CustomTokenAuth
    {
       public static void AddCustomTokenAuth(this IServiceCollection service, CustomTokenOption tokenOption)
       {
            service.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(opt =>
            {
               

                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = tokenOption.Audience[0],
                    ValidIssuer = tokenOption.Issuer,
                    IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOption.SecurityKey),
                    ClockSkew = TimeSpan.Zero,



                };
            });
        }
    }
    
}
