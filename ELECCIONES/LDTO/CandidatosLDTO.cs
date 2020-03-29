using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ELECCIONES.LDTO
{
    public class CandidatosLDTO
    {


        
        public int IdCandidatos { get; set; }

        [Required(ErrorMessage = "Informacion requerida")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Informacion requerida")]
        public string Apellido { get; set; }

        public int PartidoPertenece { get; set; }

        public int PuestoAspira { get; set; }

        [Required(ErrorMessage ="Seleccionar imagen")]
        
        public IFormFile Foto { get; set; }

        public bool? Estado { get; set; }

    }    
}
