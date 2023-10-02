using Data.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Data;
using EntregaFinalAcademiaFront.ViewModels;

namespace EntregaFinalAcademiaFront.Controllers
{
	public class UserController : Controller
	{
        private readonly IHttpClientFactory _httpClient;
        public UserController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        [Authorize]
        public IActionResult User()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> UserAddPartial([FromBody] Data.DTOs.UserDto usuario)
        {
            var usuariosViewModel = new UserViewModel();
            if (usuario != null)
            {
                usuariosViewModel = usuario;
            }

            return PartialView("~/Views/User/Partial/UserAddPartial.cshtml", usuariosViewModel);
        }

        [Authorize(Policy = "Administrador")]
        public IActionResult GuardarUsuario(Data.DTOs.UserDto usuario)
        {
            usuario.CodUsuario = 0;
            usuario.Estado = true;

            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var usuarios = baseApi.PostToApi("User/Register", usuario, token);
            return View("~/Views/User/User.cshtml");
        }
    }
}
