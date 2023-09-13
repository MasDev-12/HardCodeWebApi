using FastEndpoints.Swagger;
using FastEndpoints;
using HardCodeWebApi.ApplicationDataBase.Registation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddDataBase();
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.Title = "My Api";
        s.Version = "v1";
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(x => x.ConfigureDefaults());



app.Run();
