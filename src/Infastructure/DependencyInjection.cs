using System.Reflection;
using System.Text;
using Domain.Entities;
using Domain.Options;
using Infastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scrutor;

namespace Infastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseInMemoryDatabase("MyDb");
        });
        
        services.Configure<Jwt>(configuration.GetSection("Jwt"));
        var serviceProvider = services.BuildServiceProvider();
        var jwtConfiguration = serviceProvider.GetRequiredService<IOptions<Jwt>>().Value;

        services
            .AddAuthentication()
            .AddJwtBearer(cfr =>
            {
                cfr.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey!))
                };
            });
        
        services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        services.Scan(action =>
        {
            action
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
        
        return services;
    }
}