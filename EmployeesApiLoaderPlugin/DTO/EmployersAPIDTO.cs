using Newtonsoft.Json;

namespace EmployeesApiLoaderPlugin.DTO
{
    public sealed class EmployersAPIDTO
    {
        [JsonConstructor]
        public EmployersAPIDTO(EmployerAPIDTO[] employers)
        {
            Employers = employers;
        }

        [JsonProperty("users")]
        public EmployerAPIDTO[] Employers { get; }
    }
}
