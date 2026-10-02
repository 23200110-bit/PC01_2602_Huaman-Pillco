using Microsoft.EntityFrameworkCore;
using PC01_Huaman_pillco.Core.Core.Interfaces;
using PC01_Huaman_pillco.Core.Infrastructure.Data;
using PC01_Huaman_pillco.Core.Infrastructure.Repositories;
using PC01_Huaman_pillco.Core.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddScoped<IOrdenServicioRepository, OrdenServicioRepository>();
builder.Services.AddScoped<IOrdenServicioService, OrdenServicioService>();

builder.Services.AddDbContext<TallerDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();