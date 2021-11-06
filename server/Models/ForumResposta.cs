using System;
using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apifmu.Models
{
    public class ForumResposta
    {
        [Key]
       public int Id { get; set; }
       [ForeignKey("Forum")]
        public int ForumId { get; set; }
       public virtual Forum Forum { get; set; }
        public string Mensagem { get; set; }
        public string Nome { get; set; }    //pode ser null ou nao
        public DateTime Data { get; set; }
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }

    }
}