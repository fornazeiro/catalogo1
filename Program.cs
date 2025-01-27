using CatalogoAPI.AppServicesExtensions;
using CatalogoAPI.Endpoints;
using CatalogoAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAuthenticationJwt();

var app = builder.Build();

app.MapAutenticacaoEndpoints();
app.MapCategoriasEndpoints();
app.MapProdutosEndpoints();

var environment = app.Environment;

app.ConfigureExceptionHandler();

app.UseExceptioHandling(environment)
    .UseSwaggerMiddleware()
    .UserAppCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

