using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public interface ICepApiService
    {
        [Get("/ws/{cep}/json")]
        Task<CepResponsive> GetAddressAsync(string cep);
    }
}
