using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class LoginViewModel
{
    [Required]
    public string user {get; set;}
    
    [Required]
    public string password {get; set;}
}