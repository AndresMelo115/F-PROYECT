using System;
using System.Collections.Generic;

namespace ELECCIONES.Models
{
    public partial class Partidos
    {
        public Partidos()
        {
            Candidatos = new HashSet<Candidatos>();
        }

        public int IdPartidos { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string LogoPartido { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Candidatos> Candidatos { get; set; }
    }
}
