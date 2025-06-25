using EmployeesApiLoaderPlugin.DTO;
using Newtonsoft.Json;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using PhoneApp.Domain.Attributes;

namespace EmployeesApiLoaderPlugin
{
    [Author(Name = "Arthur Garafutdinov")]
    public sealed class Plugin : IPluggable, IDisposable
    {
        private const string RequestUri = "https://dummyjson.com/users";
        private readonly HttpClient _httpClient = new HttpClient();
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            logger.Info($"Start a sending to {RequestUri}");

            using (var response = _httpClient.GetAsync(RequestUri).GetAwaiter().GetResult())
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    var result = JsonConvert.DeserializeObject<EmployersAPIDTO>(json);

                    logger.Info($"Success! Will load {result.Employers.Length} employers.");

                    return GetEmployees(result.Employers);
                }

                logger.Error($"Error! Code: {response.StatusCode}");

                return Array.Empty<EmployeesDTO>();
            }
        }

        private static IEnumerable<EmployeesDTO> GetEmployees(EmployerAPIDTO[] employers)
        {
            foreach (var employer in employers)
                yield return new EmployeesDTO()
                {
                    Name = $"{employer.LastName} {employer.FirstName} {employer.MaidenName}",
                    Phone = employer.Phone
                };
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
