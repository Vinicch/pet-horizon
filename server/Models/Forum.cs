using System;
using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apifmu.Models
{
    public class Forum
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string Nome { get; set; }    //pode ser null ou nao
        public DateTime Data { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }


    }
}