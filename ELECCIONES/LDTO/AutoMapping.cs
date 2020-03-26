using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ELECCIONES.Models;
using ELECCIONES.LDTO;


namespace ELECCIONES.LDTO
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {


            ConfigureCandidatos();

        }


        private void ConfigureCandidatos()
        {

            CreateMap<CandidatosLDTO, Candidatos>();

            CreateMap<Candidatos, CandidatosLDTO>().ForMember(Destino =>
            Destino.Foto, Opcion => Opcion.Ignore());



        }

        
    }
}
