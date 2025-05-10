using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoProtech.API.Data;
using ProjetoProtech.API.Models;
using ProjetoProtech.API.Repositories;
using Xunit;

namespace ProjetoProtech.Tests.Repositories
{
    public class AnimeRepositoryTests
    {
        private async Task<AppDbContext> GetInMemoryDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            // Adiciona dados iniciais para testes
            context.Animes.AddRange(
                new Anime { Nome = "Naruto", Diretor = "Masashi Kishimoto", Resumo = "Ninja", Ativo = true },
                new Anime { Nome = "Bleach", Diretor = "Tite Kubo", Resumo = "Shinigami", Ativo = true }
            );

            await context.SaveChangesAsync();

            return context;
        }

        [Fact]
        public async Task GetAllAnimesAsync_ReturnsAllAnimes()
        {
            var context = await GetInMemoryDbContextAsync();
            var repo = new AnimeRepository(context);

            var animes = await repo.GetAllAnimesAsync();

            Assert.Equal(2, animes.Count());
        }

        [Fact]
        public async Task GetAnimeByIdAsync_ReturnsCorrectAnime()
        {
            var context = await GetInMemoryDbContextAsync();
            var repo = new AnimeRepository(context);

            var anime = await repo.GetAnimeByIdAsync(1);

            Assert.NotNull(anime);
            Assert.Equal("Naruto", anime.Nome);
        }

        [Fact]
        public async Task CreateAnimeAsync_AddsNewAnime()
        {
            var context = await GetInMemoryDbContextAsync();
            var repo = new AnimeRepository(context);

            var newAnime = new Anime { Nome = "One Piece", Diretor = "Eiichiro Oda", Resumo = "Pirata" };
            await repo.CreateAnimeAsync(newAnime);

            var animes = await repo.GetAllAnimesAsync();
            Assert.Equal(3, animes.Count());
        }

        [Fact]
        public async Task UpdateAnimeAsync_UpdatesExistingAnime()
        {
            var context = await GetInMemoryDbContextAsync();
            var repo = new AnimeRepository(context);

            var anime = await repo.GetAnimeByIdAsync(1);
            anime.Nome = "Naruto Shippuden";
            await repo.UpdateAnimeAsync(anime);

            var updated = await repo.GetAnimeByIdAsync(1);
            Assert.Equal("Naruto Shippuden", updated.Nome);
        }

        [Fact]
        public async Task DeleteAnimeAsync_RemovesAnime()
        {
            var context = await GetInMemoryDbContextAsync();
            var repo = new AnimeRepository(context);

            await repo.DeleteAnimeAsync(1);
            var anime = await repo.GetAnimeByIdAsync(1);

            Assert.Null(anime);
        }

        [Fact]
        public async Task GetAnimesAsync_WithFiltersAndPagination_WorksCorrectly()
        {
            var context = await GetInMemoryDbContextAsync();
            var repo = new AnimeRepository(context);

            var result = await repo.GetAnimesAsync(nome: "Naruto", pageNumber: 1, pageSize: 1);
            Assert.Single(result);
            Assert.Contains(result, a => a.Nome.Contains("Naruto"));
        }
    }
}
