using Microsoft.EntityFrameworkCore;
using ProjetoProtech.API.Data;
using ProjetoProtech.API.Repositories; // Importando a pasta Repository

var builder = WebApplication.CreateBuilder(args);

// Configurar a conex�o com o PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Registrar os reposit�rios para inje��o de depend�ncia
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();  // Registro correto para AnimeRepository

// Adicionar os servi�os do Swagger para documenta��o
builder.Services.AddControllers(); // Registro do servi�o de controllers
builder.Services.AddEndpointsApiExplorer(); // Para que os endpoints sejam explorados automaticamente
builder.Services.AddSwaggerGen(); // Gera��o de documenta��o do Swagger

// Se houver outros reposit�rios, registre-os aqui:
// builder.Services.AddScoped<IOtherRepository, OtherRepository>();

var app = builder.Build();

// Configura��es do Swagger (exibido apenas no ambiente de desenvolvimento)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Ativa o Swagger para dev
    app.UseSwaggerUI(); // Configura o SwaggerUI para a interface de documenta��o
}

// Configura��o do middleware de autoriza��o (caso necess�rio)
app.UseAuthorization();

// Mapeamento das rotas de controllers
app.MapControllers(); // Mapeia as rotas definidas nos controllers

app.Run(); // Executa a aplica��o
