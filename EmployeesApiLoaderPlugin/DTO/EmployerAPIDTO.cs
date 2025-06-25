using Newtonsoft.Json;

namespace EmployeesApiLoaderPlugin.DTO
{
    public sealed class EmployerAPIDTO
    {
        [JsonConstructor]
        public EmployerAPIDTO(string firstName, string lastName, string maidenName, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            MaidenName = maidenName;
            Phone = phone;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string MaidenName { get; }
        public string Phone { get; }
    }
}
