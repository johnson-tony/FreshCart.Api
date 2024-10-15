namespace FreshCart.Api.Model
{
    public class Cart
    {
        public int CartId { get; set; }

        public int CustId { get; set; }


        public int ProductId { get; set; }


        public int Quantity { get; set; }

        public string ShortName { get; set; }

        public DateTime AddedDate { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public string ProductName { get; set; }


    }
}
