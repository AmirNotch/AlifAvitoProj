using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Models
{
    public class Advert
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Title can not be more than 100 characters")]
        public string Title { get; set; }

        public int Cost { get; set; }
        
        [Required]
        [StringLength(2000, MinimumLength = 50, ErrorMessage = "Review Text must be between 50 and 2000 charachers")]
        public string ReviewText { get; set; }
        
        public DateTime? DatePublished { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<AdvertUser> AdvertUsers { get; set; }
        public virtual ICollection<AdvertCategory> AdvertCategories { get; set; }
    }
}
