using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apifmu.Models
{
    public class Adoption
    {
        [Key]
        public int Id { get; set; }

        public char Situation { get; set; }

        public string DisapprovalReason { get; set; }

        [ForeignKey("Ong")]
        public int OngId { get; set; }

        public Ong Ong { get; set; }

        [ForeignKey("Pet")]
        public int PetId { get; set; }

        public Pet Pet { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
