using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntregaFinalAcademiaFront.Controllers
{
	public class UserController : Controller
	{
		[Authorize]
		public IActionResult User()
		{

			return View();
		}
	}
}
