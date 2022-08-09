using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homework4.Data;
using Homework4.Models.Base;
using Homework4.Models.Enums;

namespace Homework4.Models.Domain
{
    public class Order : BaseItem
    {
        [Required(ErrorMessage = "Wrong customer name!")]
        [DisplayName("Customer Name")]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Wrong address!")]
        [DisplayName("Address")]
        [StringLength(200)]
        public string Address { get; set; }

        [Required(ErrorMessage = "You must choose one pizza type for the order!")]
        public PizzaEnum Pizza { get; set; }

        [Required(ErrorMessage = "Wrong phone number!")]
        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? AdditionalOrderInfo { get; set; }
    }
}
