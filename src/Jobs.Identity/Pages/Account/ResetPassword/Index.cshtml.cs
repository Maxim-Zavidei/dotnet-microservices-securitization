using AutoMapper;
using IdentityServerHost.Pages;
using Jobs.Identity.Models;
using Jobs.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jobs.Identity.Pages.ResetPassword;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IEmailService emailService;

    [BindProperty]
    public required InputModel inputModel { get; set; }

    public IActionResult OnGet(string token, string email, string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        var inputModel = new InputModel
        {
            Email = email,
            Token = token
        };
        return Page();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostAsync(string returnUrl)
    {
        if (!ModelState.IsValid) return Page();

        var user = await userManager.FindByEmailAsync(inputModel.Email);

        //if (user == null) RedirectToAction(ResetPasswordConfirmation);

        var resetPasswordResult = await userManager.ResetPasswordAsync(user, inputModel.Token, inputModel.Password);

        if (!resetPasswordResult.Succeeded)
        {
            foreach (var error in resetPasswordResult.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return Page();
        }

        // Redirect to reset password confirmation view;
        return Page();
    }

    public IActionResult ResetPasswordConfirmation(string returnUrl)
    {

        ViewData["ReturnUrl"] = returnUrl;
        return Page();
    }
}
