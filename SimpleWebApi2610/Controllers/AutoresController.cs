using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi2610.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi2610.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDBContext context;

        public AutoresController(ApplicationDBContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Este método trae todos los autores
        /// </summary>
        /// <returns>Puros autores</returns>
        /// <remarks>Este es un remark para información ampliada de autores de libros comunes y conocidos.</remarks>
        /// <response code="200">Todo bien eh</response>
        /// <response code="400">La liaste ah</response>   
        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.Include(x => x.Libros).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            await context.Autores.AddAsync(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Autor autor)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
                return NotFound("no se encontró el autor que se quiere actualizar");

            if (id != autor.Id)
                return BadRequest("los ids no coinciden");

            context.Autores.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Este método trae todos los autores
        /// </summary>
        /// <returns>Puros autores</returns>
        /// <remarks>Este es un remark para información ampliada de autores de libros comunes y conocidos.</remarks>
        /// <response code="200">Todo bien eh</response>
        /// <response code="400">La liaste ahsss</response>   
        /// <response code="404">No existe</response>   
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
                return NotFound($"El autor con id {id} que se quiere eliminar no existe");

            context.Autores.Remove(new Autor { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
