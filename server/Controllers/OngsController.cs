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
    [Route("ongs")]
    public class OngsController : ControllerBase
    {

        private DataContext dataContext;

        public OngsController(DataContext context)
        {
            this.dataContext = context;
        }

        //https://localhost:5001/Ongs (passar json body)
        [HttpPost]
        public async Task<ActionResult> cadastrar([FromBody] Ongs p)
        {
            try{

            dataContext.ongs.Add(p);
            await dataContext.SaveChangesAsync();
            return Created("Objeto Ongs", p);
            }catch(Exception e){
                return NotFound(e);
            }
        }

        //https://localhost:5001/Ongs
        [HttpGet]
        public async Task<ActionResult> listar()
        {
            var dados = await dataContext.ongs.ToListAsync();
            return Ok(dados);
        }

        //https://localhost:5001/Ongs/2
        [HttpGet("{codigo}")]
        public Ongs filtrar(int codigo)
        {
            Ongs p = dataContext.ongs.Find(codigo);
            return p;
        }

        //https://localhost:5001/Ongs(json completo com o codigo)
         [HttpPut]
        public async Task<ActionResult> editar([FromBody] Ongs p)
        {
            dataContext.ongs.Update(p);
            await dataContext.SaveChangesAsync();
            return Ok(p);
        }
        

        //https://localhost:5001/Ongs/1
        [HttpDelete("{codigo}")]
        public async Task<ActionResult> delete(int codigo){

            Ongs p = filtrar(codigo);
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