using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace apifmu.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
        public string Email { get; set; }
        public string  Senha { get; set; }
        public string  Endereco { get; set; }
        public string Cpf { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }

        //dados da residencia
        public string Residencia { get; set; } // Casa / Apto
        public string TamanhoResidencia { get; set; } // pequena / media / grande
        public bool? Quintal { get; set; } // sim ou nao
        public bool? Tela { get; set; } // sim ou nao
        public string Renda { get; set; } // combo 1000 a 2000 etc

//se for 
        [ForeignKey("Ongs")]
        public int? OngsId { get; set; }
         public Ongs Ong { get; set; }
    }
}