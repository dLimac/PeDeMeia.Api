using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using PeDeMeia.Infra.Data;
using PeDeMeia.Infra.Repository;
using PeDeMeia.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// --- 2. SUBSTITUA A LINHA AddSwaggerGen POR ESTE BLOCO ---
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PeDeMeia API",
        Version = "v1",
        Description = "API de controle financeiro pessoal"
    });
});
// ----------------------------------------------------------

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Database Connection
builder.Services.AddSingleton<DatabaseConnection>();
builder.Services.AddSingleton<DatabaseInitializer>();

// Repositories
builder.Services.AddScoped<PessoaRepository>();
builder.Services.AddScoped<BancoRepository>();
builder.Services.AddScoped<ReceitaRepository>();
builder.Services.AddScoped<DespesaRepository>();

// Services
builder.Services.AddScoped<PessoaService>();
builder.Services.AddScoped<BancoService>();
builder.Services.AddScoped<ReceitaService>();
builder.Services.AddScoped<DespesaService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Initialize Database
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    dbInitializer.InitializeDatabase();
}


app.UseSwagger();

// Configure a Interface Visual para ler o JSON no lugar certo
app.UseSwaggerUI(c =>
{
    // O caminho deve começar com uma barra "/"
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PeDeMeia API v1");

    // Opcional: Se quiser que o swagger abra na raiz do site (sem /swagger)
    // c.RoutePrefix = string.Empty; 
});

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Serve static files (frontend)
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();