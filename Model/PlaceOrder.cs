using System.ComponentModel.DataAnnotations;

namespace FreshCart.Api.Model
{
    public class PlaceOrder
    {
        [Key]
        public int SaleId { get; set; }

        public int CustId { get; set; }

        public DateTime SaleDate { get; set; }

        public int TotalInvoiceAmount { get; set; }

        public string Discount { get; set; }

        public string PaymentNaration { get; set; }

        public string DeliveryAddress1 { get; set; }

        public string DeliveryAddress2 { get; set; }
        
        public string DeliveryCity { get; set; }   

        public string PinCode { get; set; }

        public string DeliveryLandMark { get; set; }





    }
}
