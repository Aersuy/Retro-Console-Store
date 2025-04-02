using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Enums;

namespace RetroConsoleStoreDotDomain.User
{
    public class UDBTablecs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {  get; set; }



        [Required]
        [Display(Name = "username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Invalid username, Issue = length")]
        public string username { get; set; }




        [Required]
        [Display(Name ="password")]
        [StringLength(50,MinimumLength = 8, ErrorMessage = "Invalid Password")]
        public string password { get; set; }



        [Required]
        [Display(Name = "email")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Invalid email")]
        public string email { get; set; }

        [Display(Name = "phone")]
        public string Phone {  get; set; }
        [Display(Name = "address")]
        public string Adress { get; set; }
        [Display(Name = "ip")]
        public string LastIP { get; set; }

        [Display(Name = "reg_date")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "last_reg_date")]
        public DateTime LastRegisterDate { get; set; }


        [Display(Name = "u_level")]
        public URole level { get; set; }

    }
}
