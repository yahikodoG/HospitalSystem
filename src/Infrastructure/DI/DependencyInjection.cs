using Application.Interfaces;
using Application.Interfaces.Repositories;
using Infrastructure.Authentication;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Infrastructure.Uow;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IJwtAuthService, JwtAuthService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}