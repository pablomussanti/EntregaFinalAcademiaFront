using Data.Base;
using Data.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using EntregaFinalAcademiaFront.ViewModels;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Diagnostics.Eventing.Reader;

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
			try
			{
				var baseApi = new BaseApi(_httpClient);
				var token = await baseApi.PostToApi("Login", login);

				var resultadoLogin = token as OkObjectResult;
				var resultadoObjeto = JsonConvert.DeserializeObject<Models.LoginResponse>(resultadoLogin.Value.ToString());

				var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                JwtSecurityTokenHandler hand = new JwtSecurityTokenHandler();

                var tokenData = hand.ReadJwtToken(resultadoObjeto.Data.Token);

				ClaimsIdentity claimsIdentity = new ClaimsIdentity();

                foreach (var claim in tokenData.Claims)
                {
                    if (!claimsIdentity.HasClaim(claim.Type, claim.Value))
                        claimsIdentity.AddClaim(claim);
                }

				var claimPrincipalData = new ClaimsPrincipal(claimsIdentity);

				Claim claimRole;
				if (claimPrincipalData.Claims.ElementAt(3).Value == "1")
				{
                    claimRole = new(ClaimTypes.Role, "Administrador");
                }
				else
				{
                    claimRole = new(ClaimTypes.Role, "Consultor");
                }

				Claim claimNombre = new(ClaimTypes.Name, resultadoObjeto.Data.Name);
				Claim claimEmail = new(ClaimTypes.Email, resultadoObjeto.Data.Email);

                identity.AddClaim(claimRole);
				identity.AddClaim(claimNombre);
				identity.AddClaim(claimEmail);

				var claimPrincipal = new ClaimsPrincipal(identity);

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties
				{
					ExpiresUtc = DateTime.Now.AddHours(1),
				});

				HttpContext.Session.SetString("Token", resultadoObjeto.Data.Token);

				var homeViewModel = new HomeViewModel();
				homeViewModel.Token = resultadoObjeto.Data.Token;

				return View("~/Views/Home/Index.cshtml", homeViewModel);
			}
			catch (Exception)
			{

				throw;
			}
			
		}

		public async Task<IActionResult> CerrarSesion()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Login");
		}

	}
}
