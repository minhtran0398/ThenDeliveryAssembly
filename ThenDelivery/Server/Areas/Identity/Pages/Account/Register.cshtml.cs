using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Areas.Identity.Pages.Account
{
	[AllowAnonymous]
	public class RegisterModel : PageModel
	{
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		private readonly ILogger<RegisterModel> _logger;
		private readonly IEmailSender _emailSender;
		private readonly RoleManager<IdentityRole> _roleManager;

		public RegisterModel(
			 UserManager<User> userManager,
			 SignInManager<User> signInManager,
			 ILogger<RegisterModel> logger,
			 IEmailSender emailSender,
			 RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
			_emailSender = emailSender;
			_roleManager = roleManager;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public string ReturnUrl { get; set; }

		public IList<AuthenticationScheme> ExternalLogins { get; set; }

		public class InputModel
		{
			[Required(ErrorMessage = "Vui lòng nhập email")]
			[EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
			[Display(Name = "Địa chỉ email")]
			public string Email { get; set; }

			[Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
			[StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự và tối đa {1} ký tự.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "Mật khẩu")]
			public string Password { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "Xác nhận mật khẩu")]
			[Compare("Password", ErrorMessage = "Mật khẩu không khớp.")]
			public string ConfirmPassword { get; set; }
		}

		public async Task OnGetAsync(string returnUrl = null)
		{
			ReturnUrl = returnUrl;
			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			//await _roleManager.CreateAsync(new IdentityRole("Demo"));
			returnUrl ??= Url.Content("~/");
			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
			if (ModelState.IsValid)
			{
				var user = new User { UserName = Input.Email, Email = Input.Email };
				var result = await _userManager.CreateAsync(user, Input.Password);
				if (result.Succeeded)
				{
					result = await _userManager.AddToRoleAsync(user, Const.Role.UserRole);
					if (result.Succeeded)
					{
						_logger.LogInformation("User created a new account with password.");

						var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
						code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
						var callbackUrl = Url.Page(
							 "/Account/ConfirmEmail",
							 pageHandler: null,
							 values: new { area = "Identity", userId = user.Id, code, returnUrl },
							 protocol: Request.Scheme);

						await _emailSender.SendEmailAsync(Input.Email, "Xác nhận email của bạn",
							 $"Xác nhận email của bạn bằng cách <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>nhấn vào đây</a>.");

						if (_userManager.Options.SignIn.RequireConfirmedAccount)
						{
							return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
						}
						else
						{
							await _signInManager.SignInAsync(user, isPersistent: false);
							return LocalRedirect(returnUrl);
						}
					}
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, "Tài khoản email đã được đăng ký.");
				}
			}

			// If we got this far, something failed, redisplay form
			return Page();
		}
	}
}
