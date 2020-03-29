using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ELECCIONES.Models
{
    public partial class Candidatos
    {
        public Candidatos()
        {
            Elecciones = new HashSet<Elecciones>();
        }

        public int IdCandidatos { get; set; }
        
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int PartidoPertenece { get; set; }
        public int PuestoAspira { get; set; }
        public string FotoPerfil { get; set; }

        public bool? Estado { get; set; }

        public virtual Partidos PartidoPerteneceNavigation { get; set; }
        public virtual PuestoElecto PuestoAspiraNavigation { get; set; }
        public virtual ICollection<Elecciones> Elecciones { get; set; }
    }
}
