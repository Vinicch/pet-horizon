using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;

namespace apifmu.Models
{
    public class Imagens
    {
         [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

        public Animal Animal { get; set; }

    }
}