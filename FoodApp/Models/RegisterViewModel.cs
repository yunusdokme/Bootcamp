using System.ComponentModel.DataAnnotations;

namespace FoodApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="UserName")]
        public string? UserName{get;set;}

        [Required]
        [Display(Name ="Ad")]
        public string? Name{get;set;}

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email{get;set;}

        [Required]
        [StringLength(10, ErrorMessage ="{0} alanı en az {2} karakter olmalıdır.",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name ="Parola")]
        public string? Password{get;set;}

        [Required]
        [StringLength(10, ErrorMessage ="{0} alanı en az {2} karakter olmalıdır.",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name ="Parola")]
        public string? ConfirmPassword{get;set;}

        
    }
}