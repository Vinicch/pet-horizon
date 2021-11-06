using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apifmu.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        public string Breed { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public string Sex { get; set; }

        public string Personality { get; set; }

        public bool WasAdopted { get; set; }

        [ForeignKey("Ong")]
        public int OngId { get; set; }

        public Ong Ong { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}
