using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EBanking.Api;

public static class SwaggerConfigurator
{
    private const string BearerSecuritySchemeId = "BearerId";

    public static void AddJwtAuthenticationSupport(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition(BearerSecuritySchemeId, new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter JWT token"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = BearerSecuritySchemeId
                    }
                },
                Array.Empty<string>()
            }
        });
    }
}