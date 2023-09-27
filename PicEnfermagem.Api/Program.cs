using PicEnfermagem.Api.Extensions;
using PicEnfermagem.Api.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddSwagger();
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseSession();
app.UseAuthorization();

app.MapControllers();

app.Run();
