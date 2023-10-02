var token = getCookie("Token");
let table = new DataTable('#usuarios', {
    ajax: {
        url: `https://localhost:7282/api/User`,
        dataSrc: "data.items",
        headers: { "Authorization": "Bearer " + token }
    },
    columns: [
        { data: 'codUsuario', title: 'Id' },
        { data: 'dni', title: 'Dni' },
        { data: 'nombre', title: 'Nombre' },
        { data: 'email', title: 'Mail' },
        { data: 'clave', title: 'Clave' },
        { data: 'estado', title: 'Estado' },
        { data: 'roleId', title: 'Id Rol' },
        { data: 'role.name', title: 'Tipo Rol' },
        {
            data: function (data) {
                var botones =
                    `<td><a href='javascript:EditarUsuario(${JSON.stringify(data)})'><i class="fa-solid fa-pen-to-square editarUsuario"></i></td>` +
                    `<td><a href='javascript:EliminarUsuario(${JSON.stringify(data)})'><i class="fa-solid fa-trash eliminarUsuario"></i></td>`
                return botones;
            }
        }

    ]
});

function AgregarUsuario() {
    $.ajax({
        type: "GET",
        url: "/User/UserAddPartial",
        data: "",
        contentType: 'application/json',
        'dataType': "html",
        success: function (resultado) {
            $('#UserAddPartial').html(resultado);
            $('#usuarioModal').modal('show');
        }

    });
}