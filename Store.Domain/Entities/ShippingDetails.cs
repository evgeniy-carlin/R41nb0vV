using System.ComponentModel.DataAnnotations;
namespace Store.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Укажите имя и фамилию")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите номер склада Новой почты")]
        public string Post { get; set; }

        [Required(ErrorMessage = "Укажите номер телефона")]
        public string Phone { get; set; }

        public bool Gift { get; set; }
    }
}
