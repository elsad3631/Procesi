using System.ComponentModel.DataAnnotations;

namespace ImmobiliareApi.Entities
{
    public class RegistrationRequest
    {
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
