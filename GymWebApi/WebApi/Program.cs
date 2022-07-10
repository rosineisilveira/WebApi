using Data.Context;
using Data.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(
                x=>x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAlunoRepository,AlunoRepository>();
builder.Services.AddScoped<IPlanoRepository,PlanoRepository>();
builder.Services.AddScoped<IPagamentoRepository,PagamentoRepository>();
builder.Services.AddScoped<ITreinoRepository,TreinoRepository>();
builder.Services.AddScoped<IInstrutorRepository,InstrutorRepository>();
builder.Services.AddScoped<IExercicioRepository,ExercicioRepository>();
builder.Services.AddScoped<IMatriculaRepository,MatriculaRepository>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

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
