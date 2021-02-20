using Api.Domain.Dtos.Cep;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;
using Api.Domain.Dtos.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {

            #region User
            
            CreateMap<UserModel, UserDto>()
                .ReverseMap();
            CreateMap<UserModel, UserDtoCreate>()
                .ReverseMap();
            CreateMap<UserModel, UserDtoUpdate>()
                .ReverseMap();

            #endregion
            CreateMap<UfModel, UfDto>()
                .ReverseMap();

            CreateMap<MunicipioModel, MunicipioDto>()
                .ReverseMap();

            CreateMap<MunicipioModel, MunicipioCreateDto>()
                .ReverseMap();

            CreateMap<MunicipioModel, MunicipioUpdateDto>()
                .ReverseMap();


            CreateMap<CepModel, MunicipioCompletoDto>()
                .ReverseMap();

            CreateMap<CepModel, CepCreateDto>()
                .ReverseMap();

            CreateMap<CepModel, CepUpdateDto>()
                .ReverseMap();



        }
    }
}
