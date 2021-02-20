using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Cep;

namespace Api.Domain.Interfaces.Services.Cep
{
    public interface ICepService
    {
        Task<CepDto> Get(Guid id);

        Task<CepDto> Get(string cep);

        Task<CepCreateResultDto> Post(CepCreateDto cep);

        Task<CepUpdateResultDto> Put(CepUpdateDto cep);

        Task<bool> Delete(Guid id);

    }
}
