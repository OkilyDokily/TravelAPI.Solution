using System.ComponentModel.DataAnnotations;

namespace ReactTravelAPI.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
  }

}
