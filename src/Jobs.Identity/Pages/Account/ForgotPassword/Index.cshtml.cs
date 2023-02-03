using AutoMapper;
using IdentityServerHost.Pages;
using Jobs.Identity.Models;
using Jobs.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jobs.Identity.Pages.ForgotPassword;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IEmailService emailService;

    [BindProperty]
    public required InputModel inputModel { get; set; }

    public IActionResult OnGet(string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return Page();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostAsync(string returnUrl)
    {
        if (!ModelState.IsValid) return Page();
        var user = await userManager.FindByEmailAsync(inputModel.Email);

        if (user == null) return Redirect("/Account/ForgotPasswordConfirmation");
        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        //var callback = Url.Action(nameof(ResestPassword), "Account", new { token, email = user.Email, returnUrl }, Request.Scheme);
        var message = "Message to the user";
        // send email

        // Redirect to forgot password confirmation view;
        return Page();
    }

    public IActionResult ForgotPasswordConfirmation()
    {
        return Page();
    }
}
