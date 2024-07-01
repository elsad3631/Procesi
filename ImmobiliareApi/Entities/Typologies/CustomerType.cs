using System.ComponentModel.DataAnnotations;

namespace ImmobiliareApi.Entities.Typologies
{
    public class CustomerType : EntityBase
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

    }
}
