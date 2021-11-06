using System.ComponentModel.DataAnnotations;

namespace apifmu.Models
{
    public class Ong
    {
        [Key]
        public int Id { get; set; }

        public string Uin { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal Lat { get; set; }

        public decimal Long { get; set; }
    }
}
