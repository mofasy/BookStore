using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs.Category
{
    public class CategoryAddDTO
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(150,ErrorMessage = "The field {0} must be between {2} and {1} characters",MinimumLength =2)]
        public string Name { get; set; }
    }
}
