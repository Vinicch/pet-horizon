using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apifmu.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public int Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public string Uin { get; set; }

        public decimal Lat { get; set; }

        public decimal Long { get; set; }

        public string Residence { get; set; } // Casa / Apto

        public string ResidenceSize { get; set; } // pequena / media / grande

        public bool? HasYard { get; set; } // sim ou nao

        public bool? HasWindowBars { get; set; } // sim ou nao

        public string Income { get; set; } // combo 1000 a 2000 etc

        [ForeignKey("Ongs")]
        public int? OngId { get; set; }

        public Ong Ong { get; set; }
    }
}
