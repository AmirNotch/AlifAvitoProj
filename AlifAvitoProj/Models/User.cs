using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "First Name can not be many than 100 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Last Name can not be many than 100 characters")]
        public string LastName { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<AdvertUser> AdvertUsers { get; set; }
    }
}
