using Microsoft.EntityFrameworkCore;
using PruebaTecnica1.Aplication.Handlers.TipoGastoHandler;
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

//Repositories
builder.Services.AddScoped<ITipoGastoRepository, TipoGastoEfRepository>();
builder.Services.AddScoped<IFondoMonetarioRepository, FondoMonetarioEfRepository>();
builder.Services.AddScoped<IPresupuestoRepository, PresupuestoEfRepository>();
builder.Services.AddScoped<IRegistroGastoRepository, RegistroGastoEfRepository>();
builder.Services.AddScoped<IDepositoRepository, DepositoEfRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioEfRepository>();

// Validaciones


builder.Services.AddDbContext<AppDbContext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaulConection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
