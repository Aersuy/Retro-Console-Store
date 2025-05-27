using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroConsoleStoreDotDomain.User
{
    public class SessionT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; } // Redundant, delete when doing next db refresh.

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }

        [Required]
        public string CookieString { get; set; }

        [Required]
        public DateTime ExpireTime { get; set; }


        // Navigation property
        public virtual UDBTablecs User { get; set; }
    }
}
