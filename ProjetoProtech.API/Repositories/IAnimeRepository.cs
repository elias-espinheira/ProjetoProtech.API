using ProjetoProtech.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoProtech.API.Repositories
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<Anime>> GetAllAnimesAsync();
        Task<Anime> GetAnimeByIdAsync(int id);
        Task CreateAnimeAsync(Anime anime);
        Task UpdateAnimeAsync(Anime anime);
        Task DeleteAnimeAsync(int id);

        // Buscar animes com filtros e paginação
        Task<IEnumerable<Anime>> GetAnimesAsync(string nome = null, string diretor = null, string resumo = null);

        // Exclusão total (hard delete) e reset de IDs (uso exclusivo para homologação)
        Task DeleteAllAnimesAsync();
    }
}
