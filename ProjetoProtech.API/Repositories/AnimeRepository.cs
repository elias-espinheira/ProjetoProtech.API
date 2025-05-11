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
            return await _context.Animes
                .Where(a => a.Ativo)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Anime> GetAnimeByIdAsync(int id)
        {
            return await _context.Animes
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id && a.Ativo);
        }

        public async Task CreateAnimeAsync(Anime anime)
        {
            anime.Ativo = true;
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
                anime.Ativo = false; 
                _context.Animes.Update(anime);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Anime>> GetAnimesAsync(string nome = null, string diretor = null, string resumo = null, int? pageNumber = 1, int? pageSize = 10)
        {
            var query = _context.Animes
                .Where(a => a.Ativo) 
                .AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(a => a.Nome.ToLower().Contains(nome.ToLower()));
            }

            if (!string.IsNullOrEmpty(diretor))
            {
                query = query.Where(a => a.Diretor.ToLower().Contains(diretor.ToLower()));
            }

            if (!string.IsNullOrEmpty(resumo))
            {
                query = query.Where(a => a.Resumo.ToLower().Contains(resumo.ToLower()));
            }

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        // apenas para usuario homologacao
        public async Task DeleteAllAnimesAsync()
        {
            //string path = @"C:\TEMP\elias.txt";

            //if (!File.Exists(path)) // verifica o arquivo padrao usuário elias
            //    return;

            var animes = await _context.Animes.ToListAsync();
            _context.Animes.RemoveRange(animes);
            await _context.SaveChangesAsync(); 

            await _context.Database.ExecuteSqlRawAsync(@"ALTER SEQUENCE ""Animes_Id_seq"" RESTART WITH 1");
        }
    }
}
