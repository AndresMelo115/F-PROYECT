using System;
using System.Collections.Generic;

namespace ELECCIONES.Models
{
    public partial class Elecciones
    {
        public Elecciones()
        {
            Resultado = new HashSet<Resultado>();
        }

        public int IdElecciones { get; set; }
        public string Nombre { get; set; }
        public string FechaRealizacion { get; set; }
        public bool Estado { get; set; }
        public int IdCandidatos { get; set; }
        public int IdCiudadanos { get; set; }

        public virtual Candidatos IdCandidatosNavigation { get; set; }
        public virtual Ciudadanos IdCiudadanosNavigation { get; set; }
        public virtual ICollection<Resultado> Resultado { get; set; }
    }
}
