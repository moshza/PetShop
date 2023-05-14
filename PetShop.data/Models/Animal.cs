using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.data.Models
{
    public partial class Animal
    {
        public Animal()
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
        public int AnimalId { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }
        [StringLength(maximumLength:2000)]
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        [StringLength(200)]
        public string PhotoUrl { get; set; } = null!;

        [ForeignKey("CategoryId")]
        [InverseProperty("Animals")]
        public virtual Category Category { get; set; } = null!;
        [InverseProperty("Animal")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
