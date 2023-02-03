using System.Security.Claims;
using AutoMapper;
using IdentityModel;
using IdentityServerHost.Pages;
using Jobs.Identity.Models;
using Jobs.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jobs.Identity.Pages.Register;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IEmailService emailService;

    [BindProperty]
    public required InputModel inputModel { get; set; }
        
    public Index(IMapper mapper, UserManager<User> userManager, IEmailService emailService)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.emailService = emailService;
    }

    public IActionResult OnGet(string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return Page();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostAsync(string returnUrl)
    {
        if (!ModelState.IsValid) return Page();
        var user = mapper.Map<User>(inputModel);
        var result = await userManager.CreateAsync(user, inputModel.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return Page();
        }
        await userManager.AddToRoleAsync(user, "Guest");
        await userManager.AddClaimsAsync(user, new List<Claim>
        {
            new Claim(JwtClaimTypes.GivenName, user.FirstName),
            new Claim(JwtClaimTypes.FamilyName, user.LastName),
            new Claim(JwtClaimTypes.Role, "Guest"),
            new Claim(JwtClaimTypes.Address, user.Address)
        });

        return Redirect(returnUrl);
    }
}