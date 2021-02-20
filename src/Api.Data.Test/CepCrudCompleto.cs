using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class CepCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvide;


        public CepCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }
        [Fact(DisplayName = "CRUD de Cep")]
        [Trait("CRUD", "CEPEntity")]
        public async Task E_Possivel_Realizar_CRUD_CEP()
        {
            using (var context = _serviceProvide.GetService<MyContext>())
            {
                MunicipioImplementation _repositorioMunicipio = new MunicipioImplementation(context);
                MunicipioEntity _municipioEntity = new MunicipioEntity
                {
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 999999),
                    UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
                };

                var _registroCriado = await _repositorioMunicipio.InsertAsync(_municipioEntity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_municipioEntity.Nome, _registroCriado.Nome);
                Assert.Equal(_municipioEntity.CodIBGE, _registroCriado.CodIBGE);
                Assert.Equal(_municipioEntity.UfId, _registroCriado.UfId);
                Assert.False(_registroCriado.Id == Guid.Empty);

                CepImplementation _repositorio = new CepImplementation(context);
                CepEntity _entity = new CepEntity
                {
                    Cep = "13+481-001",
                    Logradouro = Faker.Address.StreetName(),
                    Numero = "0 até 2000",
                    MunicipioId = _registroCriado.Id

                };

                var _registroCriadoCep = await _repositorio.InsertAsync(_entity);
                Assert.NotNull(_registroCriadoCep);
                Assert.Equal(_entity.Cep, _registroCriadoCep.Cep);
                Assert.Equal(_entity.Logradouro, _registroCriadoCep.Logradouro);
                Assert.Equal(_entity.Numero, _registroCriadoCep.Numero);
                Assert.Equal(_entity.MunicipioId, _registroCriadoCep.MunicipioId);
                Assert.False(_registroCriadoCep.Id == Guid.Empty);

                _entity.Logradouro = Faker.Address.StreetName();
                _entity.Id = _registroCriadoCep.Id;
                var _registroAtualizado = await _repositorio.UpdateAsync(_entity);

                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_entity.Cep, _registroAtualizado.Cep);
                Assert.Equal(_entity.Logradouro, _registroAtualizado.Logradouro);
                Assert.Equal(_entity.Numero, _registroAtualizado.Numero);
                Assert.Equal(_entity.MunicipioId, _registroAtualizado.MunicipioId);
                Assert.True(_registroAtualizado.Id == _entity.Id);

                var _registroExiste = await _repositorio.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);

                 var _registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_registroAtualizado.Cep, _registroSelecionado.Cep);
                Assert.Equal(_registroAtualizado.Logradouro, _registroSelecionado.Logradouro);
                Assert.Equal(_registroAtualizado.Numero, _registroSelecionado.Numero);
                Assert.Equal(_registroAtualizado.MunicipioId, _registroSelecionado.MunicipioId);


                


            }
        }

    }
}
