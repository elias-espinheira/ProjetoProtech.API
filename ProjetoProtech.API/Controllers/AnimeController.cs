using Microsoft.AspNetCore.Mvc;
using ProjetoProtech.API.Models;
using ProjetoProtech.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoProtech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly IAnimeRepository _animeRepository;

        // Injeção de dependência do repositório
        public AnimeController(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anime>>> GetAllAnimes(
            [FromQuery] string nome = null,
            [FromQuery] string diretor = null,
            [FromQuery] string resumo = null,
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null)
        {
            // Chama o repositório para buscar os animes
            var animes = await _animeRepository.GetAnimesAsync(nome, diretor, resumo, pageNumber, pageSize);

            return Ok(animes); // Retorna todos os animes com código 200 (OK)
        }


        // GET: api/anime/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Anime>> GetAnimeById(int id)
        {
            var anime = await _animeRepository.GetAnimeByIdAsync(id);

            if (anime == null)
                return NotFound(); // Retorna 404 caso não encontre o anime

            return Ok(anime); // Retorna o anime com código 200
        }

        // POST: api/anime
        [HttpPost]
        public async Task<ActionResult<Anime>> CreateAnime(Anime anime)
        {
            await _animeRepository.CreateAnimeAsync(anime);
            return CreatedAtAction(nameof(GetAnimeById), new { id = anime.Id }, anime); // Retorna 201 e a URL do anime criado
        }

        // PUT: api/anime/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnime(int id, Anime anime)
        {
            if (id != anime.Id)
                return BadRequest(); // Retorna 400 caso o ID não coincida com o ID do anime enviado

            await _animeRepository.UpdateAnimeAsync(anime);
            return NoContent(); // Retorna 204 (sem conteúdo), indicando que foi atualizado com sucesso
        }

        // DELETE: api/anime/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnime(int id)
        {
            await _animeRepository.DeleteAnimeAsync(id);
            return NoContent(); // Retorna 204 caso o anime seja deletado com sucesso
        }
    }
}
