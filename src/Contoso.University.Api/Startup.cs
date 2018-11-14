using System.Reflection;
using AutoMapper;
using Contoso.University.Infra.Courses.Repositories;
using Contoso.University.Model.AccessControl.Behaviors;
using Contoso.University.Model.Courses.Commands;
using Contoso.University.Model.Courses.Repositories;
using Contoso.University.Model.Shared.Services;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;
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
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(x => x.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)
            .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(modelAssembly));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Security
            // TODO: Configure keys
            services.AddDataProtection();

            services.AddSwagger();

            // Mediator
            services.AddMediatR(currentAssembly, modelAssembly, zekApiAssembly);

            // Auto Mapper
            services.AddAutoMapper(currentAssembly, modelAssembly, zekApiAssembly); 

            Mapper.AssertConfigurationIsValid();

            RegisterCourses(services);
            RegisterShared(services);
        }

        private void RegisterShared(IServiceCollection services)
        {
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(UserOperationsMediatorBehavior<,>));
            services.AddScoped<IDomainEvents, MediatorDomainEvents>();
        }

        private void RegisterCourses(IServiceCollection services)
        {
            services.AddScoped<ICourseRepository, CourseRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseCors(_ => {
                _.AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowAnyOrigin()
                 .AllowCredentials();
            });

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });
        }
    }
}
