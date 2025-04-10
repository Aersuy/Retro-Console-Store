using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.User;

namespace RetroConsoleStoreDotDomain.Error
{
    public class ETable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ErrorId { get; set; }

        [Required]
        public string ErrorMessage { get; set; }

        [Required]
        public string Exception { get; set; }

        [Required]
        public DateTime Date { get; set; }


    }
}
