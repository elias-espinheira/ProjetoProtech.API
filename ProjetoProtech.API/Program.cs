using Microsoft.EntityFrameworkCore;
using ProjetoProtech.API.Data;
using ProjetoProtech.API.Repositories; // Importando a pasta Repository

var builder = WebApplication.CreateBuilder(args);

// Configurar a conexão com o PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Registrar os repositórios para injeção de dependência
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();  // Registro correto para AnimeRepository

// Adicionar os serviços do Swagger para documentação
builder.Services.AddControllers(); // Registro do serviço de controllers
builder.Services.AddEndpointsApiExplorer(); // Para que os endpoints sejam explorados automaticamente
builder.Services.AddSwaggerGen(); // Geração de documentação do Swagger

// Se houver outros repositórios, registre-os aqui:
// builder.Services.AddScoped<IOtherRepository, OtherRepository>();

var app = builder.Build();

// Configurações do Swagger (exibido apenas no ambiente de desenvolvimento)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Ativa o Swagger para dev
    app.UseSwaggerUI(); // Configura o SwaggerUI para a interface de documentação
}

// Configuração do middleware de autorização (caso necessário)
app.UseAuthorization();

// Mapeamento das rotas de controllers
app.MapControllers(); // Mapeia as rotas definidas nos controllers

app.Run(); // Executa a aplicação
