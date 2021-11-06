using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace apifmu.Models
{
    public class Ongs
    {
        [Key]

        public int Id { get; set; }
        public int Cnpj { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }


    }
}