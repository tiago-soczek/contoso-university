using System.Reflection;
using AutoMapper;
using Contoso.University.Api.AccessControl;
using Contoso.University.Api.Shared.Services;
using Contoso.University.Infra.Shared;
using Contoso.University.Infra.Shared.Repositories;
using Contoso.University.Model.AccessControl.Behaviors;
using Contoso.University.Model.AccessControl.Services;
using Contoso.University.Model.Courses.Commands;
using Contoso.University.Model.Shared.Repositories;
using Contoso.University.Model.Shared.Services;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Zek.Api;
using Zek.Api.Filters;

namespace Contoso.University.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Assemblies
            var currentAssembly = Assembly.GetExecutingAssembly();
            var modelAssembly = typeof(RegisterCourseCommand).Assembly;
            var zekApiAssembly = typeof(BaseController).Assembly;

            // Logging
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            // MVC
            services.AddMvc(options =>
            {
                // Validation & Exception filters
                options.Filters.Add(typeof(ValidatorActionFilter));
                options.Filters.Add(typeof(HandleErrorFilterAttribute));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(x => x.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)
            .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssembly(modelAssembly);
                    options.ImplicitlyValidateChildProperties = true;
                });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Security
            // TODO: Configure keys
            services.AddDataProtection();

            services.AddHealthChecks();

            services.AddSwaggerDocument(x => x.Title = "Contoso University");

            // Mediator
            services.AddMediatR(currentAssembly, modelAssembly, zekApiAssembly);

            // Auto Mapper
            services.AddAutoMapper(currentAssembly, modelAssembly, zekApiAssembly); 

            RegisterCourses(services);
            RegisterAccessControl(services);
            RegisterShared(services);
        }

        private void RegisterAccessControl(IServiceCollection services)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
        }

        private void RegisterShared(IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UserOperationsMediatorBehavior<,>));
            services.AddScoped<IDomainEvents, MediatorDomainEvents>();
            services.AddScoped<IDiagnosticsService, DiagnosticsService>();

            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("Default")));
        }

        private void RegisterCourses(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseCors(x => {
                x.AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowAnyOrigin()
                 .AllowCredentials();
            });

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseHealthChecks("/_healthz");
        }
    }
}
