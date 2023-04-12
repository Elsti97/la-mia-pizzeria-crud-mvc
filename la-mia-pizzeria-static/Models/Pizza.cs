using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using la_mia_pizzeria_static.Attributes;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo Nome è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il campo Nome deve contenere al massimo 50 caratteri")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Il campo Descrizione è obbligatorio")]
        [Column(TypeName = "text")]
        [NWord(5)]
        public string? Descrizione { get; set; }

        [Required(ErrorMessage = "Il campo Prezzo è obbligatorio")]
        [Range(0.01, 999.99, ErrorMessage = "Il prezzo deve essere maggiore di zero")]
        public decimal? Prezzo { get; set; }
    }
}

