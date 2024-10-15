using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreshCard.Api.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        
        public string ProductSku { get; set; }

        
        public string ProductName { get; set; }

        
        public string ProductDescription { get; set; }

        
        public string ShortName { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }

        
        public string ImageUrl { get; set; }

        
        public string DeliveryTime { get; set; }

       
        [ForeignKey("Category")]
        // Consider renaming CategoryId to CategoryId if it represents a foreign key
        public int CategoryId { get; set; }

        // Navigation property for Category
       
    }
}
