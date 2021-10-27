using SimpleWebApi2610.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi2610.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(maximumLength:15,MinimumLength = 5,ErrorMessage = "El campo {0} debe tener una longitud entre {2} y {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Titulo { get; set; }

        [Range(1,500)]
        [NotMapped] //hace que no se tenga que corresponder con una tabla en la bd
        public int NumeroPaginas { get; set; }

        [NotMapped]
        [CreditCard]
        public string TarjetaCredito { get; set; }

        [NotMapped]
        [Url]
        public string Url { get; set; }

        [NotMapped]
        [EmailAddress]
        public string Email { get; set; }



        public int AutorId { get; set; }

        //Propiedad de navegacion
        public Autor Autor { get; set; }
    }
}
