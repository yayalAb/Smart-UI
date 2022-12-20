using Application.Common;
using Application.Common.Behaviours;
using Application.Common.Service;
using Application.OperationDocuments.Queries.Common;
using Application.OperationFollowupModule;
using Application.OperationModule;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<OperationEventHandler>();
            services.AddScoped<DocumentationService>();
            services.AddScoped<DefaultCompanyService>();
            services.AddScoped<OperationService>();
            services.AddSingleton<AppdivConvertor>();
            services.AddScoped<GeneratedDocumentService>();
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));

            return services;

        }

    }
}
