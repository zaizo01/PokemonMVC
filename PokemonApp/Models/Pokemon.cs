using System.ComponentModel.DataAnnotations;

namespace PokemonApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este valor es requerido.")]
        public string Name { get; set; }
        public virtual PokemonRegion PokemonRegion { get; set; }
        [Required(ErrorMessage = "Este valor es requerido.")]
        public int PokemonRegionId { get; set; }
        public virtual  PokemonType PokemonType { get; set; }
        [Required(ErrorMessage = "Este valor es requerido.")]
        public int PokemonTypeId { get; set; }
        //[Required]
        //public string ImgPath { get; set; }
    }
}
