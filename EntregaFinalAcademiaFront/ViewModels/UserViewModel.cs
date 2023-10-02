using Data.DTOs;

namespace EntregaFinalAcademiaFront.ViewModels
{
    public class UserViewModel
    {
        public int CodUsuario { get; set; }
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public int RoleId { get; internal set; }
        public string Clave { get; set; }
        public string Email { get; set; }
        public Boolean Estado { get; set; }

        public static implicit operator UserViewModel(UserDto usuario)
        {
            var usuariosViewModel = new UserViewModel();
            usuariosViewModel.CodUsuario = usuario.CodUsuario;
            usuariosViewModel.Dni = usuario.Dni;
            usuariosViewModel.Nombre = usuario.Nombre;
            usuariosViewModel.RoleId = usuario.RoleId;
            usuariosViewModel.Clave = usuario.Clave;
            usuariosViewModel.Email = usuario.Email;
            usuariosViewModel.Estado = usuario.Estado;
            return usuariosViewModel;
        }
    }
}
