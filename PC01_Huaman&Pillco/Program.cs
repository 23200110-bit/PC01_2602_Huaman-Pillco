using Microsoft.EntityFrameworkCore;
using PC01_Huaman_pillco.Core.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

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