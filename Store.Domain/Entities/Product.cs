using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Store.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Укажите наименование товара")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Укажите положительное значение цены")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Укажите категорию")]
        public string Categoty { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
