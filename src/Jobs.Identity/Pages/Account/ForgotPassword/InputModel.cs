using System.ComponentModel.DataAnnotations;

namespace Jobs.Identity.Pages.ForgotPassword;

public class InputModel
{
    [EmailAddress]
    public required string Email { get; set; }
}
