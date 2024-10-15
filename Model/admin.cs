using System.ComponentModel.DataAnnotations;

namespace FreshCart.Api.Model
{
    public class admin
    { 
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

    }
}
