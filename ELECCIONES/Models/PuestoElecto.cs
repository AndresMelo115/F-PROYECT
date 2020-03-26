using System;
using System.Collections.Generic;

namespace ELECCIONES.Models
{
    public partial class PuestoElecto
    {
        public PuestoElecto()
        {
            Candidatos = new HashSet<Candidatos>();
        }

        public int IdPuestoE { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Candidatos> Candidatos { get; set; }
    }
}
