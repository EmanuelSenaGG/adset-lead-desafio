using API.Contexto;
using API.Mappings;
using API.Middlewares;
using API.Repositorios.Implementacoes;
using API.Repositorios.Interfaces;
using API.Services.Implementacoes;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdSetContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdSetConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AdSet API Veiculos",
        Version = "v1",
        Description = "API de gestão de veículos para resolução de desafio técnico"
    });
});

builder.Services.AddScoped<IVeiculoService, VeiculoService>();
builder.Services.AddScoped<ICorService, CorService>();
builder.Services.AddScoped<IPortalService, PortalService>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<ICorRepository, CorRepository>();
builder.Services.AddScoped<IPortalRepository, PortalRepository>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new VeiculoProfile());
    cfg.AddProfile(new FotoProfile());
    cfg.AddProfile(new OpcionalProfile());
    cfg.AddProfile(new PacoteProfile());
    cfg.AddProfile(new PortalProfile());
    cfg.AddProfile(new RelacaoVeiculoOpcionalProfile());
    cfg.AddProfile(new RelacaoVeiculoPacotePortalProfile());

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AdSet API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
