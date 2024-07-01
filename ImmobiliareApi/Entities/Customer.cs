using ImmobiliareApi.Entities.Typologies;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace ImmobiliareApi.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid Phone")]
        [StringLength(int.MaxValue, MinimumLength = 6)]
        public int? Phone { get; set; }
        public int? CustomerTypeId { get; set; }
        public virtual CustomerType? CustomerType { get; set; }


    }
}
