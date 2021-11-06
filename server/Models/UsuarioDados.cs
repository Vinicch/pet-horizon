using System.ComponentModel.DataAnnotations;

namespace apifmu.Models
{
    public class UsuarioDados
    {
        [Key]
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public int Batimento { get; set; }
        public int Temperatura { get; set; }
        public int Oxigenio { get; set; }
        public string Stress { get; set; }
        public int Passos { get; set; }
        public string Sono { get; set; }
    }

 
}