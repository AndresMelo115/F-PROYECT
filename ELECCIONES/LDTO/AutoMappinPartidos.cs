using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ELECCIONES.Models;
using ELECCIONES.LDTO;

namespace ELECCIONES.LDTO
{
    public class AutoMappinPartidos : Profile
    {

        public AutoMappinPartidos()
        {

            ConfiguracionPartidos();


        }


        private void ConfiguracionPartidos()
        {

            CreateMap<PartidosLDTO, Partidos>();
            CreateMap<Partidos, PartidosLDTO>().ForMember(Destiny =>
            Destiny.Logo, Opciones => Opciones.Ignore());
           

        }
    }
}

