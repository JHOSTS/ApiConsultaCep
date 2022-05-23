using Refit;
using System;
using System.Threading.Tasks;
using API.Model;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;

namespace API
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool stop = false;
            while (!stop)
            {
                try
                {
                    Console.WriteLine("Informe o seu Nome de Operador: ");
                    string nomeOperador = Console.ReadLine();

                    var cepClient = RestService.For<ICepApiService>("http://viacep.com.br");
                    Console.WriteLine("Informe o cep: ");

                    string cepInformado = Console.ReadLine();
                    Console.WriteLine("Verificando informações do CEP {0}... ", cepInformado);

                    var address = await cepClient.GetAddressAsync(cepInformado);

                    Console.Clear();
                    Console.Beep(1000, 100);
                    Console.Write($"\tResultado:\n\nLogradouro: {address.Logradouro},\nBairro: {address.Bairro},\nCidade: {address.Localidade}");
                    var resultCep = $"{address.Logradouro}, {address.Bairro}, {address.Localidade}.  Consultor(a): {nomeOperador}";

                    if (address.Logradouro == null)
                    {
                        Console.WriteLine("\n\nPossívelmente foi digitado algo errado, se atente e corrija, por favor.");
                        continue;
                    }

                    List<String> ceps = new List<string>();
                    ceps.Add(resultCep);


                    if (nomeOperador != null && nomeOperador != null)
                    {
                        var path = $@"C:\temp\Teste\Levantamento_{DateTime.Now:dd-MM-yyyy}\".Trim();
                        var path2 = path + "Ope_" + nomeOperador + $"-{DateTime.Now:dd-MM-yyyy}.txt".Trim();
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        var cepsJson = JsonConvert.SerializeObject(ceps, Formatting.None).Replace("\n", "");

                        File.AppendAllLines(path2, ceps);


                        Console.WriteLine("\n\nSe quiser finalizar digite 'Fechar'. Se for continuar digite 'Continuar'");
                        var fim = Console.ReadLine();
                        if (fim.ToUpper() == "Fechar".ToUpper())
                        {
                            stop = true;
                            break;
                        }
                        else
                            continue;
                    }
                }

                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("Erro na consulta do cep:\n" + e.Message);
                    Console.WriteLine("Aperte Enter para sair.");
                    Console.ReadKey();
                    break;
                }
            }
        }
    }
}
