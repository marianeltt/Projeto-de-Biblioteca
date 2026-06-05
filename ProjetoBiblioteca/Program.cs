using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using ProjetoBiblioteca.Data;
using System.Globalization;

// Compatibilidade com tipos DateTime do PostgreSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte ao padrão MVC
builder.Services.AddControllersWithViews();

var connStr =
    builder.Configuration.GetConnectionString("DefaultConnection");

// Configuração da sessão do usuário
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
});

// Configuração do Entity Framework com PostgreSQL
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(connStr));

// Configuração da cultura padrão pt-BR
var supportedCultures =
    new[] { new CultureInfo("pt-BR") };

builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
    opts.DefaultRequestCulture =
        new RequestCulture("pt-BR");

    opts.SupportedCultures =
        supportedCultures;

    opts.SupportedUICultures =
        supportedCultures;
});

var app = builder.Build();

// Tratamento de erros em produção
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Aplica localização pt-BR
app.UseRequestLocalization();

// Habilita controle de sessão
app.UseSession();

// Define Login como página inicial do sistema
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();