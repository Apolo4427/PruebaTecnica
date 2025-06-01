using Microsoft.EntityFrameworkCore;
using PruebaTecnica1.Aplication.Handlers.TipoGastoHandler;
using PruebaTecnica1.Application.Services;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;
using PruebaTecnica1.Core.Ports.Repositories;
using PruebaTecnica1.Interface.Persistence.Data;
using PruebaTecnica1.Interface.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


// configuraciones basicas iniciales
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Automapper

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CreateTipoGastoCommandHandler>();
});

// Definir la política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200") // Origen(es) permitidos
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


//Repositories
builder.Services.AddScoped<ITipoGastoRepository, TipoGastoEfRepository>();
builder.Services.AddScoped<IFondoMonetarioRepository, FondoMonetarioEfRepository>();
builder.Services.AddScoped<IPresupuestoRepository, PresupuestoEfRepository>();
builder.Services.AddScoped<IRegistroGastoRepository, RegistroGastoEfRepository>();
builder.Services.AddScoped<IDepositoRepository, DepositoEfRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioEfRepository>();

// Validaciones


// Context
builder.Services.AddDbContext<AppDbContext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection"));
});

// hasher para password
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// invocas EnsureCreated() antes de usar el contexto
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Esto creará la base de datos si no existe. 
    // Si existe, hará nada.
    dbContext.Database.EnsureCreated();
}

// Registrar user admin:
using (var scope = app.Services.CreateScope())
{
    var seederRepo = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();
    var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

    try
    {
        // Si GetByUsernameAsync no lanza excepción, significa que ya existe “admin”
        await seederRepo.GetByUsernameAsync("admin", CancellationToken.None);
    }
    catch (KeyNotFoundException)
    {
        // No existe: creamos “admin/admin”
        var usernameVo = Username.Create("admin");
        var passwordHash = PlainPassword.Create(hasher.Hash("admin"));
        var nuevoAdmin = Usuario.Create(usernameVo, passwordHash, esAdmin: true);
        await seederRepo.AddAsync(nuevoAdmin, CancellationToken.None);
    }
}

// Usar CORS *antes* de MapControllers
app.UseCors("AllowAngularDev");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
