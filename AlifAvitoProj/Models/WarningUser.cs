using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Models
{
    public class WarningUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Warning can not be more than 100 characters")]
        public string Warning { get; set; }

        public virtual Admin Admin { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
