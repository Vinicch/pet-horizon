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
    [Route("adoption")]
    public class AdoptionController : ControllerBase
    {

        private DataContext dataContext;

        public AdoptionController(DataContext context)
        {
            this.dataContext = context;
        }

        //https://localhost:5001/Animal (passar json body)
        [HttpPost]
        public async Task<ActionResult> cadastrar([FromBody] Adoption p)
        {
            try{

            dataContext.adoption.Add(p);
            await dataContext.SaveChangesAsync();
            return Created("Objeto adoption", p);
            }catch(Exception e){
                return NotFound(e);
            }
        }

        //https://localhost:5001/Animal
        [HttpGet]
        public async Task<ActionResult> listar()
        {
            var dados = await dataContext.adoption.ToListAsync();
            return Ok(dados);
        }

        //https://localhost:5001/Animal/2
        [HttpGet("{codigo}")]
        public Adoption filtrar(int codigo)
        {
            Adoption p = dataContext.adoption.Find(codigo);
            return p;
        }

        //https://localhost:5001/Animal(json completo com o codigo)
         [HttpPut]
        public async Task<ActionResult> editar([FromBody] Adoption p)
        {
            dataContext.adoption.Update(p);
            await dataContext.SaveChangesAsync();
            return Ok(p);
        }
        

        //https://localhost:5001/Animal/1
        [HttpDelete("{codigo}")]
        public async Task<ActionResult> delete(int codigo){

            Adoption p = filtrar(codigo);
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