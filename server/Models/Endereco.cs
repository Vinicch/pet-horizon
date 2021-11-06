using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using System;

namespace apifmu.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemnto { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public int Cep { get; set; }
    }
}