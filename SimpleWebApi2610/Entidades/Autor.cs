using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi2610.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        //Propiedad de navegacion
        public List<Libro> Libros { get; set; }
    }
}
