using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace ELECCIONES.LDTO
{
    public class PartidosLDTO
    {
        public int IdPartidos { get; set; }

        [Required (ErrorMessage = "Ingresar nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Favor ingrese descripcion")]
        public string Descripcion { get; set; }

        
        [Required(ErrorMessage = "Favor ingresar imagen")]
        public IFormFile Logo { get; set; }

        public bool Estado { get; set; }
    }
}
