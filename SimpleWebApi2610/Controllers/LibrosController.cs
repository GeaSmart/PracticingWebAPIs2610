using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleWebApi2610.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi2610.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : Controller
    {
        private readonly ApplicationDBContext context;
        private readonly ILogger logger;

        public LibrosController(ApplicationDBContext context,ILogger<AutoresController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get() //también se puede devolver Task<ActionResult<List<Libro>>>
        {
            logger.LogInformation("Information - Trayendo todos los registros");
            logger.LogCritical("Critical - Mensaje crítico");
            return Ok(await context.Libros.ToListAsync());
        }

        [HttpGet("{id:int}")]        
        public async Task<ActionResult<Libro>> GetTest([FromRoute]int id, [FromHeader] string header, [FromQuery]string query,[FromQuery]string query2)
        {
            return await context.Libros.FirstOrDefaultAsync();
        }

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<Libro>> Get([FromRoute] int id)
        //{
        //    var libro = await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);

        //    if (libro == null)
        //        return NotFound();

        //    return libro;
        //}        

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Libro libro)
        {
            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if (!existeAutor)
                return NotFound($"no existe el autor con id {libro.AutorId}");

            var existeAutorMismoNombre = await context.Libros.AnyAsync(x => x.Titulo == libro.Titulo);
            if (existeAutorMismoNombre)
                return BadRequest("Ya existe un libro con el mismo título");

            await context.Libros.AddAsync(libro);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Libro libro)
        {
            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if (!existeAutor)
                return NotFound($"no existe el autor con id {libro.AutorId}");

            if (id != libro.Id)
                return BadRequest($"los id no coinciden, ud ingresó {id} y {libro.Id}.");

            context.Update(libro);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Libros.AnyAsync(x => x.Id == id);
            if (!existe)
                return NotFound($"No existe el libro con id {id}");

            context.Remove(new Libro { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
