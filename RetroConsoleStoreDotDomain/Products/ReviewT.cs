using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RetroConsoleStoreDotDomain.User;

namespace RetroConsoleStoreDotDomain.Products
{
    public class ReviewT
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public int rating { get; set; }

        [Required]
        public string ReviewText { get; set; }

        [Required]
        public string Title { get; set; }

        public bool VerifiedPurchase { get; set; }

        [Required]
        public DateTime DateTime { get; set; }


        // Navigation properties
        public virtual ProductTypeT Product { get; set; }  // Product reviewd.
        public virtual UDBTablecs User {  get; set; }  // User doing the reviewing
    }
}
