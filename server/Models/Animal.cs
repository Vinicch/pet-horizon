using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apifmu.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Sex { get; set; }
        public string Personalidade { get; set; }
        public bool StatusAdocao { get; set; }

        [ForeignKey("Ongs")]
        public int OngsId { get; set; }
        public Ongs Ong { get; set; }

        [ForeignKey("Usuario")]
        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}