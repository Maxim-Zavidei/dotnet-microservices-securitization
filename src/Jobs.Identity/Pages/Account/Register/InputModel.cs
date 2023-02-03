using System.ComponentModel.DataAnnotations;

namespace Jobs.Identity.Pages.Register;

public class InputModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public required string Address { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare(nameof(this.Password), ErrorMessage = "The Password and confirmation password do not match")]
    public required string ConfirmPassword { get; set; }
}
