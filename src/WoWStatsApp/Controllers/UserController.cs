using Microsoft.AspNetCore.Mvc;
using WowStats.Common.Models.User;
using WowStats.Common.Services.Abstractions;

namespace WoWStatsApp.Controllers
{
	public class UserController : Controller
	{
		private IWarshipsUserService _userService;

		public UserController(IWarshipsUserService warshipsUserService)
		{
			_userService = warshipsUserService;
		}

		public async Task<IActionResult> Index()
		{
			return View("Index");
		}

		public async Task<IActionResult> Get(string userId)
		{
			WarshipsUser? matchedUser;

			if (string.IsNullOrWhiteSpace(userId))
			{
				return BadRequest($"parameter: '{nameof(userId)}', cannot be null, empty or whitespace.");
			}

			try
			{
				matchedUser = await _userService.GetWarshipsUserAsync(userId);
			}
			catch (ArgumentNullException argumentException)
			{
				return BadRequest(argumentException.Message);
			}
			catch (Exception)
			{
				return new StatusCodeResult(500);
			}

			return Json(matchedUser);
		}
	}
}
