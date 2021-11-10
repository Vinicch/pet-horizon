using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apifmu.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public char Sex { get; set; }

        public string Personality { get; set; }

        [Required]
        public bool WasAdopted { get; set; }

        [Required]
        public string Photo { get; set; }

        [ForeignKey("Ong")]
        public int OngId { get; set; }

        public Ong Ong { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}
