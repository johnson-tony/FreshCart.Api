using System.ComponentModel.DataAnnotations;

namespace FreshCart.Api.Model
{
    public class CustomerRegister
    {

        [Key]
        public int custId { get; set; }

        public string custName { get; set;}

        public long phoneNumber { get; set;}

        public string password { get; set;}
    }
}
