using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class UserDto
    {
        public int CodUsuario { get; set; }
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public int RoleId { get; internal set; }
        public string Clave { get; set; }
        public string Email { get; set; }
        public Boolean Estado { get; set; }
    }
}
