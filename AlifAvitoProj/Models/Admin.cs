using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "First Name can not be many than 100 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль админа должен содержать между 6 - 50 символов")]
        public string Password { get; set; }

        public ICollection<WarningUser> WarningUsers { get; set; }
    }
}
