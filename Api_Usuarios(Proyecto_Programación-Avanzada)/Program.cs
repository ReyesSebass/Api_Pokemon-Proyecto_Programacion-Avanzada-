using Api_Usuarios_Proyecto_Programación_Avanzada_.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add conexion to the server
//"ConnectionString": {
//"ConexionBD": "mysql://root:HCPRCPTglAJIKicrKKpnjUrnQGLQmDjC@junction.proxy.rlwy.net:10318/railway;"
//},
var connectionString = builder.Configuration.GetConnectionString("ConexionBD");

//Add to the service
builder.Services.AddDbContext<ConexionDbContext>(
    options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))

    ));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
