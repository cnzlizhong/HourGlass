using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MinGlass.API.Middleware;
using MinGlass.API.Requests;
using MinGlass.API.services;
using MinGlass.API.UseCases;
using MinGlass.Repository;
using MinGlass.Repository.Context;
using MinGlass.Repository.Interfaces;
using System.Reflection;

namespace MinGlass.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MigrateAppContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("Admin")));
            services.AddDbContext<ClientAppContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("User")));

            services.AddCors();

            services.AddControllers();

            RegisterRepositories(services);

            services.AddMediatR(GetAssembliesToScan());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MinGlass.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MinGlass.API v1"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => options
                .WithOrigins(new[] { "http://localhost:3000" })
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtService, JwtService>();
        }

        private static Assembly[] GetAssembliesToScan()
        {
            return new[]
            {
                typeof(Startup).GetTypeInfo().Assembly,
                typeof(RegisterUserRequest).GetTypeInfo().Assembly,
                typeof(RegisterUserUseCase).GetTypeInfo().Assembly
            };
        }
    }
}
