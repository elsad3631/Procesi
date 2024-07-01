using System.ComponentModel.DataAnnotations;

namespace ImmobiliareApi.Entities.Typologies
{
    public class BuildingType : EntityBase
    {
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;
    }
}
