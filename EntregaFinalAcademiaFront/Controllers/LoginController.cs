using Data.Base;
using Data.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using EntregaFinalAcademiaFront.ViewModels;

namespace EntregaFinalAcademiaFront.Controllers
{
    public class LoginController : Controller
    {

        private readonly IHttpClientFactory _httpClient;
        public LoginController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Login()
        {
            return View();
        }

		public async Task<IActionResult> Ingresar(LoginDto login)
		{
			var baseApi = new BaseApi(_httpClient);
			var token = await baseApi.PostToApi("Login", login);

			var resultadoLogin = token as OkObjectResult;
			var resultadoObjeto = JsonConvert.DeserializeObject<Models.LoginResponse>(resultadoLogin.Value.ToString());


			var homeViewModel = new HomeViewModel();
			homeViewModel.Token = resultadoObjeto.Data.Token;

			return View("~/Views/Home/Index.cshtml", homeViewModel);
		}

		public async Task<IActionResult> CerrarSesion()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Login");
		}

	}
}
