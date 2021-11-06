using System;
using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;

namespace apifmu.Models
{
    public class Pessoa
    {
        [Key]
       public int Id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public int idade { get; set; }

    }
}