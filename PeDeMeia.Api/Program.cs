
using FluentValidation.AspNetCore;
using PeDeMeia.Api.FluentValidation.Validators;

namespace PeDeMeia.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BancoInputModelValidator>());
            builder.Services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DespesaInputModelValidator>());
            builder.Services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PessoaInputModelValidator>());
            builder.Services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ReceitaInputModelValidator>());
            builder.Services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CartaoInputModelValidator>());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();


        //   app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapFallbackToFile("index.html");

            app.MapControllers();

            app.Run();
        }
    }
}
