let filtrarPorDni = document.querySelector("#filtrarPorDni");
let buscarInquilinoDNI = document.querySelector("#buscarInquilinoDNI");
buscarInquilinoDNI.addEventListener("input", (e) => {
    let dni = e.target.value;
    if(e.target.value === ""){
        axios.get("http://localhost:5203/Inquilino/GetInquilinos", {params:{dni: dni}})
        .then(res => {
            let tablaInquilinos = document.querySelector("#tablaInquilinos");
            let inquilinos = res.data;
            llenarTabla(tablaInquilinos, inquilinos);
        })
        .catch(err => console.log(err));
    }
})
filtrarPorDni.addEventListener("click", (e) => {
    let dni = buscarInquilinoDNI.value;
    axios.get("http://localhost:5203/Inquilino/GetInquilinos", {
        params:{dni: dni}
    })
    .then(res => {
        let tablaInquilinos = document.querySelector("#tablaInquilinos");
        let inquilinos = res.data;
        llenarTabla(tablaInquilinos, inquilinos);
    })
    .catch(err => console.log(err));
})

const llenarTabla = (tabla, elementos) => {
    tabla.innerHTML = "";
    let maqueta = ""; 
    for(let i of elementos){
        maqueta += `<tr>
            <td>${i.dni}</td>
            <td>${i.apellido}</td>
            <td>${i.nombre}</td>
            <td>${i.telefono}</td>
            <td>${i.correo}</td>
            <td class="d-flex align-items-center w-auto">
                <a href="/Inquilino/Editar/${i.idInquilino}" class="btn btn-editbt mx-1">
                    <i class="fa-solid fa-pen-to-square"></i>
                </a>
                <form asp-action="Borrar" class="mx-1" method="post" style="display:inline;">
                    <input type="hidden" name="id" value="${i.idInquilino}" />
                    <button type="submit" class="btn btn-borrarbt" title="Borrar">
                        <i class="fa-solid fa-trash"></i>
                    </button>
                </form>
                <a class="btn btn-detallebt mx-1" href="/Inquilino/Detalle/${i.idInquilino}">
                    <i class="fa-solid fa-list"></i>
                </a>
            </td>
        </tr>`
    }
    tabla.innerHTML = maqueta;
}