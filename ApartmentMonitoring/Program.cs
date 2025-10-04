using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Extension;
using ApartmentMonitoring.Infrastructure.BackgroundServices;
using ApartmentMonitoring.Infrastructure.SignalR;
using ApartmentMonitoring.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureAuthorization(builder.Configuration);

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSignalR();
builder.Services.AddHostedService<NotificationJob>();
builder.Services
	.ConfigureService(builder.Configuration);
builder.Services.AddMemoryCache();

// Ðåãèñòðàöèÿ middleware

builder.Services.ConfigureDbSettings(builder.Configuration);
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials()
			.SetIsOriginAllowed(_ => true);
	});
});

var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<UserActivityMiddleware>();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
	});
}


app.UseHttpsRedirection();
app.UseRouting();

;
app.UseAuthentication();
app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
	endpoints.MapHub<NotificationHub>("/notificationHub");
	endpoints.MapHub<ChatHub>("/ñhatHubHub");
});
#pragma warning restore ASP0014 // Suggest using top level route registrations
app.MapControllers();

app.Run();
