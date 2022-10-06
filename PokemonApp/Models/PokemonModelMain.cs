namespace PokemonApp.Models
{
    public class PokemonModelMain
    {
        public IEnumerable<Pokemon> Pokemon { get; set; }
        public IEnumerable<PokemonRegion> PokemonRegion { get; set; }
        public IEnumerable<PokemonType> PokemonType { get; set; }
    }
}
