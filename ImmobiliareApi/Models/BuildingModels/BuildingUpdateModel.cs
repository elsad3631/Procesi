using System.ComponentModel.DataAnnotations;

namespace ImmobiliareApi.Models.BuildingModels
{
    public class BuildingUpdateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [AllowedValues("In Vendita", "Affitto")]
        public string State { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Mq is required")]
        public int Mq { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }
        public int BuildingTypeId { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;

    }
}
