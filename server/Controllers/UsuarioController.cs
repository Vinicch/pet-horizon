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
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {

        private DataContext dataContext;

        public UsuarioController(DataContext context)
        {
            this.dataContext = context;
        }

        //https://localhost:5001/usuario (passar json body)
        [HttpPost]
        public async Task<ActionResult> cadastrar([FromBody] Usuario p)
        {
            try{

            dataContext.usuario.Add(p);
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
            var dados = await dataContext.usuario.ToListAsync();
            return Ok(dados);
        }

        //https://localhost:5001/usuario/2
        [HttpGet("{codigo}")]
        public Usuario filtrar(int codigo)
        {
            Usuario p = dataContext.usuario.Find(codigo);
            return p;
        }

        //https://localhost:5001/usuario(json completo com o codigo)
         [HttpPut]
        public async Task<ActionResult> editar([FromBody] Usuario p)
        {
            dataContext.usuario.Update(p);
            await dataContext.SaveChangesAsync();
            return Ok(p);
        }
        

        //https://localhost:5001/usuario/1
        [HttpDelete("{codigo}")]
        public async Task<ActionResult> delete(int codigo){

            Usuario p = filtrar(codigo);
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