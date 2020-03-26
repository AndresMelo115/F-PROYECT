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
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public IFormFile Logo { get; set; }

        public bool Estado { get; set; }
    }
}
