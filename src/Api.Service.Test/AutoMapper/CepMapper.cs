using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Cep;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class CepMapper : BaseTesteService
    {
        [Fact(DisplayName = "É Possível Mapear os Modelos de Cep")]
        public void E_Possivel_Mapear_os_Modelos_Cep()
        {
            var model = new CepModel
            {
                Id = Guid.NewGuid(),
                Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                Logradouro = Faker.Address.StreetName(),
                Numero = "",
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                MunicipioId = Guid.NewGuid()
            };
            var listaEntity = new List<CepEntity>();

            for (int i = 0; i < 5; i++)
            {
                var item = new CepEntity
                {
                    Id = Guid.NewGuid(),
                    Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.RandomNumber.Next(1, 10000).ToString(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    MunicipioId = Guid.NewGuid(),
                    Municipio = new MunicipioEntity
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        CodIBGE = Faker.RandomNumber.Next(1, 10000),
                        UfId = Guid.NewGuid(),
                        Uf = new UfEntity
                        {
                            Id = Guid.NewGuid(),
                            Nome = Faker.Address.UsState(),
                            Sigla = Faker.Address.UsState().Substring(1, 3)
                        }
                    }

                };
                listaEntity.Add(item);
            }

            //model => entity
            var entity = Mapper.Map<CepEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Logradouro, model.Logradouro);
            Assert.Equal(entity.Numero, model.Numero);
            Assert.Equal(entity.Cep, model.Cep);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            //entity para Dto
            var cepDto = Mapper.Map<CepDto>(entity);
            Assert.Equal(cepDto.Id, entity.Id);
            Assert.Equal(cepDto.Logradouro, entity.Logradouro);
            Assert.Equal(cepDto.Numero, entity.Numero);
            Assert.Equal(cepDto.Cep, entity.Cep);

            var cepCompletoDto = Mapper.Map<CepDto>(listaEntity.FirstOrDefault());

            Assert.Equal(cepCompletoDto.Id, listaEntity.FirstOrDefault().Id);
            Assert.Equal(cepCompletoDto.Cep, listaEntity.FirstOrDefault().Cep);
            Assert.Equal(cepCompletoDto.Logradouro, listaEntity.FirstOrDefault().Logradouro);
            Assert.Equal(cepCompletoDto.Numero, listaEntity.FirstOrDefault().Numero);

            Assert.NotNull(cepCompletoDto.Municipio);
            Assert.NotNull(cepCompletoDto.Municipio.Uf);

            var listaDto = Mapper.Map<List<CepDto>>(listaEntity);
            Assert.True(listaDto.Count() == listaEntity.Count());

            for (int i = 0; i < listaDto.Count(); i++)
            {
                Assert.Equal(listaDto[i].Id, listaEntity[i].Id);
                Assert.Equal(listaDto[i].Cep, listaEntity[i].Cep);
                Assert.Equal(listaDto[i].Logradouro, listaEntity[i].Logradouro);
                Assert.Equal(listaDto[i].Numero, listaEntity[i].Numero);

            }

            var cepCreateResultDto = Mapper.Map<CepCreateResultDto>(entity);
            Assert.Equal(cepCreateResultDto.Id, entity.Id);
            Assert.Equal(cepCreateResultDto.Cep, entity.Cep);
            Assert.Equal(cepCreateResultDto.Logradouro, entity.Logradouro);
            Assert.Equal(cepCreateResultDto.Numero, entity.Numero);
            Assert.Equal(cepCreateResultDto.CreateAt, entity.CreateAt);

            var cepUpdateResultDto = Mapper.Map<CepUpdateResultDto>(entity);
            Assert.Equal(cepUpdateResultDto.Id, entity.Id);
            Assert.Equal(cepUpdateResultDto.Cep, entity.Cep);
            Assert.Equal(cepUpdateResultDto.Logradouro, entity.Logradouro);
            Assert.Equal(cepUpdateResultDto.Numero, entity.Numero);
            Assert.Equal(cepUpdateResultDto.UpdateAt, entity.UpdateAt);

            //dto para model
            cepDto.Numero = "";
            var cepModel = Mapper.Map<CepModel>(cepDto);
            Assert.Equal(cepModel.Id, cepDto.Id);
            Assert.Equal(cepModel.Cep, cepDto.Cep);
            Assert.Equal(cepCompletoDto.Logradouro, cepDto.Logradouro);
            Assert.Equal("S/N", cepDto.Numero);

        }

    }
}
