

using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel;

public class LoginViewModel {
    [Required]
    public string User {get; set;}
    [Required]
    public string Password {get; set;}
}