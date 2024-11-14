using Carter;
using DonacionSangre.Domain.Interfaces;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using DonacionSangre.Domain.Interfaces.SqlServerRepository;
using DonacionSangre.Domain.Services;
using DonacionSangre.Infrastructure;
using DonacionSangre.Infrastructure.MongoRepositories.ReservaDonacionMongoRepository;
using DonacionSangre.Infrastructure.MongoRepositories.SolicitudDonacionMongoRepository;
using DonacionSangre.Infrastructure.Repositories.MongoDbRepository;
using DonacionSangre.Infrastructure.Services.NotificacionesAutomaticas;
using DonacionSangre.Infrastructure.Services.Sincronizacion;
using DonacionSangre.Infrastructure.SqlServerRepositories.Donante;
using DonacionSangre.Infrastructure.SqlServerRepositories.Repository;
using DonacionSangre.Infrastructure.SqlServerRepositories.Reserva;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
// Agregar SQL Server DbContext
builder.Services.AddDbContext<SqlDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

// Configuración de los Repositorios
builder.Services.AddScoped<IDonanteRepository, DonanteRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<INotificacionAutomaticaService, NotificacionAutomaticaService>();

//CORS
builder.Services.AddCors(options =>
options.AddDefaultPolicy(config =>
config
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin()));

// Configuración del Repositorio de MongoDB
builder.Services.AddSingleton<IReservaDonacionMongoRepository, ReservaDonacionMongoRepository>();
builder.Services.AddSingleton<IDonanteMongoRepository, DonanteMongoRepository>();
builder.Services.AddSingleton<ISolicitudDonacionMongoRepository, SolicitudDonacionMongoRepository>();
builder.Services.AddSingleton<IPersonaMongoRepository, PersonaMongoRepository>();
builder.Services.AddSingleton<ICentroSaludMongoRepository, CentroSaludMongoRepository>();
builder.Services.AddSingleton<IReservaDonacionService, ReservaDonacionService>();

// Servicio de Sincronización
builder.Services.AddScoped<SynchronizationService>();
builder.Services.AddCarter();

// Agregar MediatR para CQRS
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar controladores y otros servicios MVC si es necesario
builder.Services.AddControllers();

//Repositorio SQL server generico 
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configurar middleware de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();  // Habilita Swagger para generar los documentos
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapCarter();
app.MapControllers();

app.Run();
