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
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int PartidoPertenece { get; set; }
        public int PuestoAspira { get; set; }

        public IFormFile Foto { get; set; }

        public bool? Estado { get; set; }

    }    
}
