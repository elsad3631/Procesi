using ImmobiliareApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace ImmobiliareApi.Models.TypologiesModels.BuildingTypeModels
{
    public class BuildingTypeSelectModel : EntityBase
    {
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

    }
}
