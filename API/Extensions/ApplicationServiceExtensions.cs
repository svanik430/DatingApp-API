using API.Data;
using API.Interfaces;
using API.Servcies;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApllicationServices(this IServiceCollection servcies, IConfiguration config)
        {
            servcies.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            servcies.AddCors(opts =>
            {
                opts.AddPolicy("AllowAngularApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                    });
            });
            servcies.AddScoped<ITokenServices, TokenServcies>();
            servcies.AddScoped<IUserRepository, UserRepository>();
            servcies.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return servcies;
        }
    }
}
