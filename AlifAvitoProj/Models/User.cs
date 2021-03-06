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
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль пользователя должен содержать между 6 - 50 символов")]
        public string Password { get; set; }

        [Required]
        public int PhoneNumber { get; set; }
        public virtual City City { get; set; }
        public virtual WarningUser WarningUser { get; set; }
        public virtual ICollection<AdvertUser> AdvertUsers { get; set; }
    }
}
