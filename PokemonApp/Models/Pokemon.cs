using System.ComponentModel.DataAnnotations;

namespace PokemonApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este valor es requerido.")]
        public string Name { get; set; }
        public PokemonRegion pokemonRegion { get; set; }
        public PokemonType pokemonType { get; set; }
    }
}
