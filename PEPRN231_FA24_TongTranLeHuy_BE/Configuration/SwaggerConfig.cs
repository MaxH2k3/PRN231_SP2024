using Microsoft.OpenApi.Models;


namespace PEPRN231_FA24_TongTranLeHuy_BE.Configuration
{
	public static class SwaggerConfig
	{
		public static void AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(swagger =>
			{
				swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "WatercolorsPainting2024", Version = "v1" });
				swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter a valid token using the Bearer scheme (\"bearer {token}\")",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "Security",
					Scheme = "Bearer"
				});
				swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});

			});
		}
	}
}
