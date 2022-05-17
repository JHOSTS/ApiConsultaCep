using Refit;
using System;
using System.Threading.Tasks;

namespace API
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var cepClient = RestService.For<ICepApiService>("http://viacep.com.br");
                Console.WriteLine("Informe o cep: ");

                string cepInformado = Console.ReadLine();
                Console.WriteLine("Verificando informações do CEP {0}... ", cepInformado);

                var address = await cepClient.GetAddressAsync(cepInformado);

                Console.Clear();
                Console.Write($"\tResultado:\n\nLogradouro: {address.Logradouro},\nBairro: {address.Bairro},\nCidade: {address.Localidade}");
                Console.ReadKey();
            }
            catch (Exception e)
            {

                Console.WriteLine("Erro na consulta do cep: " + e.Message);
            }
        }
    }
}
