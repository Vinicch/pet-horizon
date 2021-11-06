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
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {

        private DataContext dataContext;

        public ClienteController(DataContext context)
        {
            this.dataContext = context;
        }

        //https://localhost:5001/cliente (passar json body)
        [HttpPost]
        public async Task<ActionResult> cadastrar([FromBody] Cliente p)
        {
            try{

            dataContext.cliente.Add(p);
            await dataContext.SaveChangesAsync();
            return Created("Objeto Cliente", p);
            }catch(Exception e){
                return NotFound(e);
            }
        }

        //https://localhost:5001/cliente
        [HttpGet]
        public async Task<ActionResult> listar()
        {
            var dados = await dataContext.cliente.ToListAsync();
            return Ok(dados);
        }

        //https://localhost:5001/cliente/2
        [HttpGet("{codigo}")]
        public Cliente filtrar(int codigo)
        {
            Cliente p = dataContext.cliente.Find(codigo);
            return p;
        }

        //https://localhost:5001/cliente(json completo com o codigo)
         [HttpPut]
        public async Task<ActionResult> editar([FromBody] Pessoa p)
        {
            dataContext.pessoa.Update(p);
            await dataContext.SaveChangesAsync();
            return Ok(p);
        }
        

        //https://localhost:5001/cliente/1
        [HttpDelete("{codigo}")]
        public async Task<ActionResult> delete(int codigo){

            Cliente p = filtrar(codigo);
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