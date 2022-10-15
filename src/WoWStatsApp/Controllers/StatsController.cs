using Microsoft.AspNetCore.Mvc;

namespace WoWStatsApp.Controllers
{
	public class StatsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
