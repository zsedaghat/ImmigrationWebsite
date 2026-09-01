using System.ComponentModel.DataAnnotations;

namespace ImmigrationWebsite.Web.Models.ViewModels;

public class ReviewCreateViewModel
{
    [Required(ErrorMessage = "Please enter your name")]
    public string Name { get; set; } = null!;


    public string? Country { get; set; }


    [Required(ErrorMessage = "Please enter your review")]
    public string Comment { get; set; } = null!;


    [Range(1, 5, ErrorMessage = "Please select a rating")]
    public int Rating { get; set; } = 5;
  

    public IFormFile? Image { get; set; }
}