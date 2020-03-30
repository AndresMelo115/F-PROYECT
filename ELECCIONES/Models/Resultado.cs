using System;
using System.Collections.Generic;

namespace ELECCIONES.Models
{
    public partial class Resultado
    {
        public int IdResultado { get; set; }
        public int? IdElecciones { get; set; }
        public int? IdCandidatos { get; set; }
        public int? IdCiudadanos { get; set; }

        public virtual Candidatos IdCandidatosNavigation { get; set; }
        public virtual Ciudadanos IdCiudadanosNavigation { get; set; }
        public virtual Elecciones IdEleccionesNavigation { get; set; }
    }
}
