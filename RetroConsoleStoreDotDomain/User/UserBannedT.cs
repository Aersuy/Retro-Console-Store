using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Enums;
using RetroConsoleStoreDotDomain.User;

namespace RetroConsoleStoreDotDomain.Model.User
{
    public class UserBannedT
    {
        [Key]
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Reason { get; set; }
        [Required]  
        public DateTime DateBanned { get; set; }
        public DateTime? DateUnbanned { get; set; }

        [Required]
        public string LastIp { get; set; }

        [Required]
        public URole roleBeforeBeingBanned { get; set; }

        [Required]
        [ForeignKey("AdmBanner")]
        public int AdminID { get; set; }



        // Navigation properties

        public virtual UDBTablecs User {  get; set; }
        public virtual UDBTablecs AdmBanner { get; set; }
    }
}
