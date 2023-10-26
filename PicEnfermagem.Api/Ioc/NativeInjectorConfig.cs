using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Application.Services;
using PicEnfermagem.Infraestrutura.Repositories;
using PicEnfermagem.Infraestrutura.Context;
using PicEnfermagem.Domain.Entities;
using PicEnfermagem.Identity.Services;
using PicEnfermagem.Api.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PicEnfermagem.Api.Ioc;

public static class NativeInjectorConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Context
        services.AddDbContext<PicEnfermagemDb>
            (opts => opts
            .UseNpgsql(configuration
            .GetConnectionString("connection")));

        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //Services
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IEmailSenderService, EmailSenderService>();

        services.AddTransient(provider => new Lazy<IIdentityService>(() => provider.GetRequiredService<IIdentityService>()));
        services.AddScoped<IAnswerService, AnswerService>();

        //Repositories
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IAlternativeRepository, AlternativeRepository>();
        services.AddScoped<IGameSettingRepository, GameSettingRepository>();

        //Cors
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
          builder => builder
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
        });

        services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PicEnfermagemDb>()
                .AddDefaultTokenProviders();

        services.AddAuthentication(configuration);

    }
}
