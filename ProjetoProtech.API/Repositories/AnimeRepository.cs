using Microsoft.EntityFrameworkCore;
using ProjetoProtech.API.Data;
using ProjetoProtech.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoProtech.API.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AppDbContext _context;

        public AnimeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Anime>> GetAllAnimesAsync()
        {
            return await _context.Animes.ToListAsync();
        }

        public async Task<Anime> GetAnimeByIdAsync(int id)
        {
            return await _context.Animes.FindAsync(id);
        }

        public async Task CreateAnimeAsync(Anime anime)
        {
            _context.Animes.Add(anime);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAnimeAsync(Anime anime)
        {
            _context.Animes.Update(anime);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnimeAsync(int id)
        {
            var anime = await _context.Animes.FindAsync(id);
            if (anime != null)
            {
                _context.Animes.Remove(anime);
                await _context.SaveChangesAsync();
            }
        }

        // Implementação do método GetAnimesAsync com filtros e paginação
        public async Task<IEnumerable<Anime>> GetAnimesAsync(string nome = null, string diretor = null, string resumo = null, int? pageNumber = 1, int? pageSize = 10)
        {
            var query = _context.Animes.AsQueryable();

            // Filtro por nome
            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(a => a.Nome.Contains(nome));
            }

            // Filtro por diretor
            if (!string.IsNullOrEmpty(diretor))
            {
                query = query.Where(a => a.Diretor.Contains(diretor));
            }

            // Filtro por resumo
            if (!string.IsNullOrEmpty(resumo))
            {
                query = query.Where(a => a.Resumo.Contains(resumo));
            }

            // Paginação
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }
    }
}
