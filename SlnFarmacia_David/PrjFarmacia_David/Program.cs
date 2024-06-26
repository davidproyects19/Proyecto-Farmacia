using Microsoft.EntityFrameworkCore;
using PrjFarmacia_David.DAO;
using PrjFarmacia_David.Models;

var builder = WebApplication.CreateBuilder(args);

string cad_cn = builder.Configuration.GetConnectionString("cn1");

builder.Services.AddDbContext<FARMACIABDContext>(
    opt => opt.UseSqlServer(cad_cn));

//DAO
builder.Services.AddScoped<UsuariosDAO>();
builder.Services.AddScoped<CarritoDao>();
builder.Services.AddScoped<ClientesDAO>();
builder.Services.AddScoped<ProductosDAO>();
builder.Services.AddScoped<VentasDAO>();

// establecer el tiempo máximo de una variable de sesion
builder.Services.AddSession(
    s => s.IdleTimeout = TimeSpan.FromMinutes(20));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// activar las sesiones
app.UseSession();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default2",
    pattern: "{controller=Usuario}/{action=ValidarIngreso}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
