namespace ProjetoProtech.API.Models
{
    public class Anime
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Resumo { get; set; }
        public string Diretor { get; set; }
        public bool Ativo { get; set; } = true; // Para exclusão lógica
    }
}
