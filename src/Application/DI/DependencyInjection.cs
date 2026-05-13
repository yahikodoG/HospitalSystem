using Application.Factories;
using Application.Interfaces;
using Application.Interfaces.Factories;
using Application.Interfaces.Services;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication (this IServiceCollection services)
    {
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IUserFactory, UserFactory>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}