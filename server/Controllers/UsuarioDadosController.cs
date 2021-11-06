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
    [Route("usuariodados")]
    public class UsuarioDadosController : ControllerBase
    {

        private DataContext dataContext;

        public UsuarioDadosController(DataContext context)
        {
            this.dataContext = context;
        }

        //https://localhost:5001/usuariodados(passar json body)
        [HttpPost]
        public async Task<ActionResult> cadastrar([FromBody] UsuarioDados p)
        {
            try{

            dataContext.usuariodados.Add(p);
            await dataContext.SaveChangesAsync();
            return Created("Objeto usuario", p);
            }catch(Exception e){
                return NotFound(e);
            }
        }

        //https://localhost:5001/usuario
        [HttpGet]
        public async Task<ActionResult> listar()
        {
            var dados = await dataContext.usuariodados.ToListAsync();
            return Ok(dados);
        }

        //https://localhost:5001/usuario/2
        [HttpGet("{codigo}")]
        public UsuarioDados filtrar(int codigo)
        {
            UsuarioDados p = dataContext.usuariodados.Find(codigo);
            return p;
        }

        //https://localhost:5001/usuario(json completo com o codigo)
         [HttpPut]
        public async Task<ActionResult> editar([FromBody] UsuarioDados p)
        {
            dataContext.usuariodados.Update(p);
            await dataContext.SaveChangesAsync();
            return Ok(p);
        }
        

        //https://localhost:5001/usuario/1
        [HttpDelete("{codigo}")]
        public async Task<ActionResult> delete(int codigo){

            UsuarioDados p = filtrar(codigo);
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