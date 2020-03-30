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
        public DateTime FechaRealizacion { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Resultado> Resultado { get; set; }
    }
}
