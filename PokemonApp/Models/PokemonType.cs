using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonApp.Models
{
    public partial class PokemonType
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Este valor es requerido.")]
        public string Name { get; set; }
    }
}
