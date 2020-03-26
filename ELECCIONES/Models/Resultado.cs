using System;
using System.Collections.Generic;

namespace ELECCIONES.Models
{
    public partial class Resultado
    {
        public int IdResultado { get; set; }
        public int ResultadoTotal { get; set; }
        public int IdElecciones { get; set; }

        public virtual Elecciones IdEleccionesNavigation { get; set; }
    }
}
