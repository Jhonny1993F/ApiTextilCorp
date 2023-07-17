using ApiTextilCorp.Data;
using ApiTextilCorp.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<TextilCorpDataContext>(builder.Configuration.GetConnectionString("TextilCorpConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapVentaEndpoints();

app.MapUsuarioEndpoints();

app.MapProductoEndpoints();

app.MapMarcaEndpoints();

app.MapClienteEndpoints();

app.MapCategoriaEndpoints();

app.MapCarritoEndpoints();

app.Run();
