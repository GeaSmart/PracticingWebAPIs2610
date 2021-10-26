using Microsoft.AspNetCore.Mvc;
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
            return new List<Autor>()
            {
                new Autor{ Id = 1, Nombre = "Gerson" },
                new Autor{ Id = 2, Nombre = "Luis" },
                new Autor{ Id = 3, Nombre = "Antonia" }
            };
        }
    }
}
