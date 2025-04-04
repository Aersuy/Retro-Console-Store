using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Enums;
using RetroConsoleStoreDotDomain.User;

namespace RetroConsoleStoreDotDomain.Logs
{
    public class LTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }


        [Required]
        public int UserId { get; set; }


        [Required]
        public string UserName { get; set; }


        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string Type { get; set; }

        public string UserIp {  get; set; }






    }
}
