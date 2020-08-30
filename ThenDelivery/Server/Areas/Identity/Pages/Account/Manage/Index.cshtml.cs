using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Areas.Identity.Pages.Account.Manage
{
	public partial class IndexModel : PageModel
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public IndexModel(
			 UserManager<User> userManager,
			 SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[Display(Name = "Địa chỉ email")]
		public string Email { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel
		{
			[Display(Name = "Tên người dùng")]
			public string Username { get; set; }

			[Phone]
			[Display(Name = "Số điện thoại")]
			public string PhoneNumber { get; set; }
		}

		private async Task LoadAsync(User user)
		{
			var userName = await _userManager.GetUserNameAsync(user);
			var email = await _userManager.GetEmailAsync(user);
			var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

			Email = email;

			Input = new InputModel
			{
				PhoneNumber = phoneNumber,
				Username = userName
			};
		}

		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			await LoadAsync(user);
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			if (!ModelState.IsValid)
			{
				await LoadAsync(user);
				return Page();
			}

			var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
			var userName = await _userManager.GetUserNameAsync(user);
			if (Input.PhoneNumber != phoneNumber)
			{
				var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
				if (!setPhoneResult.Succeeded)
				{
					StatusMessage = "Lỗi cập nhật.";
					return RedirectToPage();
				}
			}
			if (Input.Username != userName)
			{
				var setUserName = await _userManager.SetUserNameAsync(user, Input.Username);
				if (!setUserName.Succeeded)
				{
					StatusMessage = "Lỗi cập nhật.";
					return RedirectToPage();
				}
			}

			await _signInManager.RefreshSignInAsync(user);
			StatusMessage = "Thông tin tài khoản của bạn đạ được cập nhật";
			return RedirectToPage();
		}
	}
}
