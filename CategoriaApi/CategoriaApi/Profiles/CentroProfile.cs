using AutoMapper;
using CategoriaApi.Data.Dto.CentroDto;
using CategoriaApi.Model;

namespace CategoriaApi.Profiles
{
    public class CentroProfile: Profile
    {

        public CentroProfile()
        {
            CreateMap<CreateCentroDto, CentroDeDistribuicao>();
            CreateMap<UpdateCentroDto, CentroDeDistribuicao>();
            CreateMap<CentroDeDistribuicao, ReadCentroDto>();
        }
    }
}
