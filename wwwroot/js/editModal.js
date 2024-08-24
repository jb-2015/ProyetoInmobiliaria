
$(document).ready(function () {
    // Al hacer clic en el botón de editar
    $('.btn-editar').click(function () {
        // Obtén los datos de la fila correspondiente
        var row = $(this).closest('tr');
        var id = row.find('td:eq(0)').text();
        var dni = row.find('td:eq(1)').text();
        var apellido = row.find('td:eq(2)').text();
        var nombre = row.find('td:eq(3)').text();
        var telefono = row.find('td:eq(4)').text();
        var estado = row.find('td:eq(5)').text();

        // Rellena el formu
        $('#editId').val(id);
        $('#editDni').val(dni);
        $('#editApellido').val(apellido);
        $('#editNombre').val(nombre);
        $('#editTelefono').val(telefono);
        $('#editEstado').val(estado);

        // Muestra el modal
        $('#editModal').modal('show');
    });

    // clicK boton de eliminar
    $('.btn-eliminar').click(function () {
        if (confirm('¿Estás seguro de que deseas eliminar este registro?')) {
            // borrado logico
            alert('Registro eliminado (borrado lógico realizado)');
        }
    });
});
