using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Application.Services;
using PicEnfermagem.Infraestrutura.Repositories;
using Microsoft.AspNetCore.Identity;
using PicEnfermagem.Infraestrutura.Context;
using PicEnfermagem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Identity.Services;
using PicEnfermagem.Api.Extensions;

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
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IAnswerService, AnswerService>();

        //Repositories
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();

        //Cors
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
          builder => builder
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
        });
        services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddRoles<IdentityRole>()
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<PicEnfermagemDb>()
        .AddUserManager<UserManager<ApplicationUser>>()
        .AddSignInManager<SignInManager<ApplicationUser>>()
        .AddDefaultTokenProviders();

        services.AddAuthentication(configuration);
        services.AddAuthorizationPolicies();

    }
}
