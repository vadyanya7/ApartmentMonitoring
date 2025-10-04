using ApartmentMonitoring.Application.Services.Implementation;
using ApartmentMonitoring.Application.Services.Interfaces;
using ApartmentMonitoring.Entity.Repository;
using ApartmentMonitoring.Infrastructure.Context;
using ApartmentMonitoring.Infrastructure.Repository;
using ApartmentMonitoring.Infrastructure.Services;
using ApartmentMonitoring.Infrastructure.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using ApartmentMonitoring.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApartmentMonitoring.Infrastructure.DbContexts;

namespace ApartmentMonitoring.Extension
{
	public static class ConfigureServiceExtension
	{
		public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
			services.AddScoped<IMailSender, MailSender>();

			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IApartmentRepository, ApartmentRepository>();
			services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();
			services.AddTransient<INotificationRepository, NotificationRepository>();
			services.AddTransient<IUserActivityRepository, UserActivityRepository>();
			services.AddTransient<IBannerRepository, BannerRepository>();
			services.AddTransient<IChatMessageRepository, ChatMessageRepository>();
			services.AddTransient<IChatRepository, ChatRepository>();

			services.AddSingleton<IUserConnectionTracker, UserConnectionTracker>();
			services.AddTransient<IHubNotifier, HubNotifier>();

			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IApartmentService, ApartmentService>();
			services.AddTransient<ISubscriptionService, SubscriptionService>();
			services.AddTransient<INotificationService, NotificationService>();
			services.AddTransient<IUserActivityTrackerService, UserActivityTrackerService>();
			services.AddTransient<IAdminService, AdminService>();
			services.AddTransient<ChatService>();

			return services;
		}

		public static IServiceCollection ConfigureDbSettings(this IServiceCollection services, IConfiguration configuration)
		{
			var connection = configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<DataBaseContext>(options =>
				options.UseNpgsql(connection));

			services.AddDbContext<SupabaseContext>(options =>
				options.UseNpgsql(configuration.GetConnectionString("SupabaseDb")));
			return services;
		}

		public static IServiceCollection ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });

				// ? Добавляем схему авторизации
				c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = Microsoft.OpenApi.Models.ParameterLocation.Header,
					Description = "Введите JWT токен в формате: Bearer {your token}"
				});

				// ? Добавляем требование безопасности
				c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
						{
							{
								new Microsoft.OpenApi.Models.OpenApiSecurityScheme
								{
									Reference = new Microsoft.OpenApi.Models.OpenApiReference
									{
										Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
										Id = "Bearer"
									}
								},
								Array.Empty<string>()
							}
						});
			});

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer("Bearer", options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = configuration["Jwt:Issuer"],
						ValidAudience = configuration["Jwt:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
					};
				});

			services.AddAuthorization(options =>
				{
					options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
				});

			return services;
		}
	}
}
