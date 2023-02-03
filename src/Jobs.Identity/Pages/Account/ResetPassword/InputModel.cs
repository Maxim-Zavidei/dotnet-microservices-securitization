using System.ComponentModel.DataAnnotations;

namespace Jobs.Identity.Pages.ResetPassword;

public class InputModel
{
    [EmailAddress]
    public required string Email { get; set; }

    public required string Token { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation passowrd do not match.")]
    public string ConfirmPassword { get; set; } = null!;
}
