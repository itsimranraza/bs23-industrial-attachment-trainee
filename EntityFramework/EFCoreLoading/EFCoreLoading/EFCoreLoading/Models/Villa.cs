using System.ComponentModel.DataAnnotations;

namespace EFCoreLoading.Models
{
    public class Villa
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }

        public ICollection<VillaAmenity> VillaAmenities { get; set; }
    }
}
