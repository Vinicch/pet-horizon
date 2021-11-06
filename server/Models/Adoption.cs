using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace apifmu.Models
{
    public class Adoption
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Ongs")]
        public int OngsId { get; set; }
        public Ongs Ong { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [ForeignKey("Animal")]
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public char situation { get; set; }
        public string DisapprovalReason { get; set; }


    }
}