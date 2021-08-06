using System;
using System.Collections.Generic;
using System.Reflection;
using JetShop.RentalCars.Application.CarRent;
using JetShop.RentalCars.Application.CarRent.GetAllCarRents;
using JetShop.RentalCars.Application.CarRent.RentCar;
using JetShop.RentalCars.Application.CarRent.ReturnCar;
using JetShop.RentalCars.Application.Cars;
using JetShop.RentalCars.Application.Cars.GetAllCarDetails;
using JetShop.RentalCars.Domain.CarRents;
using JetShop.RentalCars.Domain.Cars;
using JetShop.RentalCars.Domain.Configuration;
using JetShop.RentalCars.Infrastructure.Persistence.Cars;
using JetShop.RentalCars.Infrastructure.Persistence.Configuration;
using JetShop.RentalCars.Infrastructure.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JetShop.RentalCars.Web.DependencyResolve
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiCompositionRoot(
            this IServiceCollection services)
            => services
                .AddDbContexts()
                .AddQueryHandlers()
                .AddInfrastructuralServices();

        public static IServiceCollection AddDbContexts(
            this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(opt => opt.UseInMemoryDatabase("InMemoryDatabase"));

            return services;
        }

        public static IServiceCollection AddInfrastructuralServices(
            this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarRentRepository, CarRentRepository>();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();

            return services;
        }

        public static IServiceCollection AddQueryHandlers(
            this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetAllCarDetailsQuery, IEnumerable<CarDetailsDto>>, GetAllCarDetailsQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllCarRentsQuery, IEnumerable<CarRentDto>>, GetAllCarRentsQueryHandler>();
            services.AddScoped<IRequestHandler<CarRentCommand, Guid>, CarRentCommandHandler>();
            services.AddScoped<IRequestHandler<CarReturnCommand, CarRentDto>, CarReturnCommandHandler>();


            return services;
        }

    }
}
