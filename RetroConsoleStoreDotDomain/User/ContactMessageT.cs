using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroConsoleStoreDotDomain.User
{
    public class ContactMessageT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string message { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Navigation property to the User entity
        public virtual UDBTablecs User { get; set; }
    }
}
