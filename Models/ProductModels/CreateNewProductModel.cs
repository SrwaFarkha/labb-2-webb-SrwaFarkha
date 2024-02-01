using System.ComponentModel.DataAnnotations;

namespace Models.ProductModels
{
    public class CreateNewProductModel
    {
        [Display(Name = "Produktnamn")]
        [Required(ErrorMessage = "Du måste ange produktnamn!")]
        public string ProductName { get; set; }
        [Display(Name = "Produktbeskrivning")]
        [Required(ErrorMessage = "Du måste ange produktbeskrivning!")]
        public string ProductDescription { get; set; }
        [Display(Name = "Pris")]
        [Required(ErrorMessage = "Du måste ange pris!")]
        public decimal Price { get; set; }
        [Display(Name = "Kategori")]
        public CategoryModel Category { get; set; }
        [Display(Name = "Status")]

        public bool Discontinued { get; set; }
    }
}
