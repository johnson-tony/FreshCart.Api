using System.ComponentModel.DataAnnotations;

namespace FreshCard.Api.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int ParentCategoryId { get; set; }
    }
}
