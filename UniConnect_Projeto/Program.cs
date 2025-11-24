using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using UniConnect_Projeto.Data;
using UniConnect_Projeto.Services;

namespace UniConnect_Projeto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(opt =>
                 {
                     opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                     opt.JsonSerializerOptions.WriteIndented = true;
                 });

            builder.Services.AddDbContext<DataContext>( // DbContext e SQL Server registrados
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Permite o EF saber qual banco usar


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<AlunoService>();
            builder.Services.AddScoped<CursoService>();
            builder.Services.AddScoped<DisciplinaService>();
            builder.Services.AddScoped<ProfessorService>();
            builder.Services.AddScoped<TurmaAlunoService>();
            builder.Services.AddScoped<TurmaProfessorService>();
            builder.Services.AddScoped<TurmaService>();
            builder.Services.AddScoped<GrupoService>();
            builder.Services.AddScoped<MaterialService>();
            builder.Services.AddScoped<RecomendacaoService>();

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
        }
    }
}
