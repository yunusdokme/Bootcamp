

using System.ComponentModel.DataAnnotations;
using FoodApp.Entity;

namespace FoodApp.Models
{

    public class FoodCreateViewModel{
        public int FoodId{get;set;}

        [Required]
        [Display(Name ="Başlık")]
        public string? Title {get;set;}

        [Required]
        [Display(Name = "İçerik")]
        public string? Content { get;set;}
        [Required]
        [Display(Name = "Url")]
        public string? Url {get;set;}

        [Required]
        [Display(Name ="Fiyat")]
        public int Price{get;set;}
        public List<Category> Categories {get;set;} = new();

    }
}