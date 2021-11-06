using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using apifmu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apifmu.Models;
using Microsoft.AspNetCore.Cors;

namespace apifmu.Controllers
{
    [Controller]
    [Route("pessoa")]
    public class PessoaController : ControllerBase
    {

        private DataContext dataContext;

        public PessoaController(DataContext context)
        {
            this.dataContext = context;
        }

        //https://localhost:5001/pessoa (passar json body)
        [HttpPost]
        public async Task<ActionResult> cadastrar([FromBody] Pessoa p)
        {
            dataContext.pessoa.Add(p);
            await dataContext.SaveChangesAsync();
            return Created("Objeto Pessoa", p);
        }

        //https://localhost:5001/pessoa
        [HttpGet]
        public async Task<ActionResult> listar()
        {
            var dados = await dataContext.pessoa.ToListAsync();
            return Ok(dados);
        }

        //https://localhost:5001/pessoa/2
        [HttpGet("{codigo}")]
        public Pessoa filtrar(int codigo)
        {
            Pessoa p = dataContext.pessoa.Find(codigo);
            return p;
        }

        //https://localhost:5001/pessoa(json completo com o codigo)
         [HttpPut]
        public async Task<ActionResult> editar([FromBody] Pessoa p)
        {
            dataContext.pessoa.Update(p);
            await dataContext.SaveChangesAsync();
            return Ok(p);
        }
        

        //https://localhost:5001/pessoa/1
        [HttpDelete("{codigo}")]
        public async Task<ActionResult> delete(int codigo){

            Pessoa p = filtrar(codigo);
            if(p != null){
                dataContext.Remove(p);
                await dataContext.SaveChangesAsync();
                return Ok();
            }else{
                return NotFound();
            }

        }
    }
}