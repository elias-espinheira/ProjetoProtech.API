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

        public AnimeController(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anime>>> GetAllAnimes(
            [FromQuery] string nome = null,
            [FromQuery] string diretor = null,
            [FromQuery] string resumo = null
           )
        {
            var animes = await _animeRepository.GetAnimesAsync(nome, diretor, resumo);
            return Ok(animes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Anime>> GetAnimeById(int id)
        {
            var anime = await _animeRepository.GetAnimeByIdAsync(id);
            if (anime == null || anime.Ativo == false)
                return NotFound(new { mensagem = $"Anime com ID {id} não encontrado ou inativo." });

            return Ok(anime);
        }

        [HttpPost]
        public async Task<ActionResult<Anime>> CreateAnime(Anime anime)
        {
            anime.Ativo = true; // Define como ativo por padrão
            await _animeRepository.CreateAnimeAsync(anime);
            return CreatedAtAction(nameof(GetAnimeById), new { id = anime.Id }, anime);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnime(int id, Anime anime)
        {
            var animeExistente = await _animeRepository.GetAnimeByIdAsync(id);
            if (animeExistente == null)
                return NotFound(new { mensagem = $"Anime com ID {id} não encontrado." });

            animeExistente.Nome = anime.Nome;
            animeExistente.Resumo = anime.Resumo;
            animeExistente.Diretor = anime.Diretor;
            animeExistente.Ativo = anime.Ativo;

            await _animeRepository.UpdateAnimeAsync(animeExistente);

            return Ok(new { mensagem = $"Anime com ID {animeExistente.Id} atualizado com sucesso!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnime(int id)
        {
            var anime = await _animeRepository.GetAnimeByIdAsync(id);
            if (anime == null)
                return NotFound(new { mensagem = $"Anime com ID {id} não encontrado." });

            // Exclusão lógica
            anime.Ativo = false;
            await _animeRepository.UpdateAnimeAsync(anime);

            return Ok(new { mensagem = $"Anime '{anime.Nome}' desativado com sucesso." });
        }

        //apenas para homologacao
        [HttpDelete("delete-all")]
        public async Task<IActionResult> DeleteAllAnimes()
        {
            string path = @"C:\TEMP\elias.txt";

            if (!System.IO.File.Exists(path))
                return Unauthorized(new { mensagem = "Arquivo de autorização não encontrado." });

            await _animeRepository.DeleteAllAnimesAsync();
            return Ok(new { mensagem = "Todos os animes foram deletados com sucesso!" });
        }

    }
}
