using ImmobiliareApi.Entities.Typologies;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ImmobiliareApi.Entities
{
    public class Building
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [AllowedValues("In Vendita", "Affitto")]
        public string? State { get; set; }
        public string? Address { get; set; }

        [Required(ErrorMessage = "Mq is required")]
        public int? Mq { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public int? Price { get; set; }
        public int? BuildingTypeId { get; set; }
        public virtual BuildingType? BuildingType { get; set; }




    }
}
