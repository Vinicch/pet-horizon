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

        public double Lat { get; set; }

        public double Long { get; set; }
    }
}
