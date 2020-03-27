﻿using System;
using System.Collections.Generic;

namespace ELECCIONES.Models
{
    public partial class Ciudadanos
    {
        public Ciudadanos()
        {
            Elecciones = new HashSet<Elecciones>();
        }

        public int IdCiudadanos { get; set; }
        public long Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Elecciones> Elecciones { get; set; }
    }
}
