using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using System;

namespace apifmu.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Profissao { get; set; }

        public string Email { get; set; }
        public string  Senha { get; set; }

    }
}

// {
//     "nome":   "Laila",
//     "datanascimento": "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
//     "email":    "laivavnobrega@hotmail.com"
//     "senha":    "123"
// }

// dotnet ef migrations add nome_da_Migratiuon
// dotnet ef database update