using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class MunicipioMapper: BaseTesteService
    {
        [Fact(DisplayName="É Possível Mapear os Modelos de Municipio")]
        public void E_Possivel_Mapear_os_Modelos_Municipio()
        {
             var model = new MunicipioModel
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1,1000),
                UfId = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

             var listaEntity = new List<MunicipioEntity>();
            for (int i = 0; i < 5; i++)
            {
                var item = new MunicipioEntity
                {
                    Id = Guid.NewGuid(),
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1,1000),
                UfId = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                Uf = new UfEntity
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1, 3)
                    }

                };
                listaEntity.Add(item);
            }

              //model => entity
            var entity = Mapper.Map<MunicipioEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.CodIBGE, model.CodIBGE);
            Assert.Equal(entity.Nome, model.Nome);
            Assert.Equal(entity.UfId, model.UfId);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);


             //entity para dto
            var userDto = Mapper.Map<MunicipioDto>(entity);
            Assert.Equal(userDto.Id, entity.Id);
            Assert.Equal(userDto.Nome, entity.Nome);
            Assert.Equal(entity.CodIBGE, entity.CodIBGE);
            Assert.Equal(entity.UfId, entity.UfId);

            var userDtoCompleto = Mapper.Map<MunicipioCompletoDto>(listaEntity.FirstOrDefault());
            Assert.Equal(userDtoCompleto.Id, listaEntity.FirstOrDefault().Id);
            Assert.Equal(userDtoCompleto.CodIBGE, listaEntity.FirstOrDefault().CodIBGE);
            Assert.Equal(userDtoCompleto.Nome, listaEntity.FirstOrDefault().Nome);
            Assert.Equal(userDtoCompleto.UfId, listaEntity.FirstOrDefault().UfId);

            Assert.NotNull(userDtoCompleto.Uf);

              var listaDto = Mapper.Map<List<MunicipioDto>>(listaEntity);
            Assert.True(listaDto.Count() == listaEntity.Count());
            
            for(int i = 0; i < listaDto.Count(); i++) 
            {
                Assert.Equal(listaDto[i].Id, listaEntity[i].Id);
                Assert.Equal(listaDto[i].Nome, listaEntity[i].Nome);
                Assert.Equal(listaDto[i].CodIBGE, listaEntity[i].CodIBGE);
                Assert.Equal(listaDto[i].UfId, listaEntity[i].UfId);
            }

            var userCreateResultDto = Mapper.Map<MunicipioCreateResultDto>(entity);
            Assert.Equal(userCreateResultDto.Id, entity.Id);
            Assert.Equal(userCreateResultDto.Nome, entity.Nome);
            Assert.Equal(userCreateResultDto.CodIBGE, entity.CodIBGE);
            Assert.Equal(userCreateResultDto.UfId, entity.UfId);
            Assert.Equal(userCreateResultDto.CreateAt, entity.CreateAt);

            var userUpdateResultDto = Mapper.Map<MunicipioUpdateResultDto>(entity);
            Assert.Equal(userUpdateResultDto.Id, entity.Id);
            Assert.Equal(userUpdateResultDto.Nome, entity.Nome);
            Assert.Equal(userUpdateResultDto.CodIBGE, entity.CodIBGE);
            Assert.Equal(userUpdateResultDto.UfId, entity.UfId);
            Assert.Equal(userUpdateResultDto.UpdateAt, entity.UpdateAt);

            //Dto para Model
            var userModel = Mapper.Map<MunicipioModel>(userDto);
            Assert.Equal(userModel.Id, userDto.Id);
            Assert.Equal(userModel.Nome, userDto.Nome);
            Assert.Equal(userModel.CodIBGE, userDto.CodIBGE);
            Assert.Equal(userModel.UfId, userDto.UfId);

            var userCreateDto = Mapper.Map<MunicipioCreateDto>(userModel);
            Assert.Equal(userCreateDto.Nome, userModel.Nome);
            Assert.Equal(userCreateDto.CodIBGE, userModel.CodIBGE);
            Assert.Equal(userCreateDto.UfId, userModel.UfId);

            var UserDtoUpdate = Mapper.Map<MunicipioUpdateDto>(userModel);
            Assert.Equal(UserDtoUpdate.Id, userModel.Id);
            Assert.Equal(UserDtoUpdate.Nome, userModel.Nome);
            Assert.Equal(UserDtoUpdate.CodIBGE, userModel.CodIBGE);
            Assert.Equal(UserDtoUpdate.UfId, userModel.UfId);



            
        }
    }
}
