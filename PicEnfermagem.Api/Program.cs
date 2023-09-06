using Microsoft.EntityFrameworkCore;
using PicEnfermagem.Application.Interfaces;
using PicEnfermagem.Application.Interfaces.Repository;
using PicEnfermagem.Application.Services;
using PicEnfermagem.Infraestrutura.Context;
using PicEnfermagem.Infraestrutura.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Services
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

//Connection Ef core
builder.Services.AddDbContext<PicEnfermagemDb>(opts => opts.UseNpgsql(builder.Configuration.GetConnectionString("connection")));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
