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
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAuthentication(configuration);
        services.AddAuthorizationPolicies();
        services.AddDistributedMemoryCache();


        //Services
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IIdentityService, IdentityService>();

        //Repositories
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        //Context
        services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddRoles<IdentityRole>()
          .AddDefaultTokenProviders()
          .AddEntityFrameworkStores<PicEnfermagemDb>()
          .AddUserManager<UserManager<ApplicationUser>>()
          .AddSignInManager<SignInManager<ApplicationUser>>()
          .AddDefaultTokenProviders();

        services.AddDbContext<PicEnfermagemDb>
            (opts => opts
            .UseNpgsql(configuration
            .GetConnectionString("connection")));


        //Cors
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

    }
}
