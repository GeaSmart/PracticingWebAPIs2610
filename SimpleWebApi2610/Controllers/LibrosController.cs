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
    public class LibrosController : Controller
    {
        private readonly ApplicationDBContext context;

        public LibrosController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [HttpGet("listado")]
        [HttpGet("/milistado")]
        public async Task<ActionResult<List<Libro>>> Get()
        {
            return await context.Libros.ToListAsync();
        }

        [HttpGet("primero")]        
        public async Task<ActionResult<Libro>> GetFirst()
        {
            return await context.Libros.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var libro = await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);

            if (libro == null)
                return NotFound();

            return libro;
        }

        [HttpGet("{titulo}")]
        public async Task<ActionResult<Libro>> Get(string titulo)
        {
            var libro = await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Titulo.Contains(titulo));

            if (libro == null)
                return NotFound();

            return libro;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if (!existeAutor)
                return NotFound($"no existe el autor con id {libro.AutorId}");

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
