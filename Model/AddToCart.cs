using System;
using System.ComponentModel.DataAnnotations;

namespace FreshCart.Api.Model
{
    public class AddToCart
    {
        [Key]
        public int CartId { get; set; }

        public int CustId { get; set; }


        public int ProductId { get; set; }

        
        public int Quantity { get; set; }

        
        public DateTime AddedDate { get; set; }
    }
}
