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
    [Route("forum")]
    public class ForumController : ControllerBase
    {

        private DataContext dataContext;

        public ForumController(DataContext context)
        {
            this.dataContext = context;
        }

        //https://localhost:5001/forum (passar json body)
        [HttpPost]
        public async Task<ActionResult> cadastrar([FromBody] Forum p)
        {
            try{

            dataContext.forum.Add(p);
            await dataContext.SaveChangesAsync();
            return Created("Objeto Forum", p);
            }catch(Exception e){
                return NotFound(e);
            }
        }

        //https://localhost:5001/forum
        [HttpGet]
        public async Task<ActionResult> listar()
        {
            var dados = await dataContext.forum.ToListAsync();
            return Ok(dados);
        }

        //https://localhost:5001/forum/2
        [HttpGet("{codigo}")]
        public Forum filtrar(int codigo)
        {
            Forum p = dataContext.forum.Find(codigo);
            return p;
        }

        //https://localhost:5001/forum(json completo com o codigo)
         [HttpPut]
        public async Task<ActionResult> editar([FromBody] Forum p)
        {
            dataContext.forum.Update(p);
            await dataContext.SaveChangesAsync();
            return Ok(p);
        }
        

        //https://localhost:5001/forum/1
        [HttpDelete("{codigo}")]
        public async Task<ActionResult> delete(int codigo){

            Forum p = filtrar(codigo);
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