//----------------------FILTRO POR INMUEBLES DISPONIBLES----------------------//
let filtroPorDisponibles = document.querySelector("#filtroPorDisponibles");
filtroPorDisponibles.addEventListener("change", (e) => {
    let chequeado = e.target.checked;
    if(!chequeado){
        axios("http://localhost:5203/Inmueble/GetNoDisponibles")
        .then(res => llenarTablaInmuebles(res.data))
        .catch(err => console.log(err));
    }else{
        axios("http://localhost:5203/Inmueble/GetDisponibles")
        .then(res => llenarTablaInmuebles(res.data))
        .catch(err => console.log(err));
    }
})

//----------------------FILTRO POR PROPIETARIOS----------------------//
let selectFiltroPorPropietario = document.querySelector("#selectFiltroPorPropietario");
selectFiltroPorPropietario.addEventListener("change", () => {
    let idPropietario = selectFiltroPorPropietario.selectedOptions[0].value;
    axios("http://localhost:5203/Inmueble/GetPorPropietario", {params:{idPropietario: idPropietario}})
    .then(res => llenarTablaInmuebles(res.data))
    .catch(err => console.log(err));
})

//----------------------FILTRO INMUEBLES SIN CONTRATO EN DETERMINADAS FECHAS----------------------//
let fechaInicioFiltroInmueble = document.querySelector("#fechaInicioFiltroInmueble");
let fechaFinFiltroInmueble = document.querySelector("#fechaFinFiltroInmueble"); 
let buscarFiltroFechas = document.querySelector("#buscarFiltroFechas");
buscarFiltroFechas.addEventListener("click", () => {
    if(fechaInicioFiltroInmueble.value !== "" && fechaFinFiltroInmueble.value !== ""){
        const fechaInicio = new Date(fechaInicioFiltroInmueble.value).toISOString().split('T')[0];
        const fechaFin = new Date(fechaFinFiltroInmueble.value).toISOString().split('T')[0];
        axios("http://localhost:5203/Inmueble/GetPorFechasDisponibles", {params:{fechaInicio, fechaFin}})
        .then(res => {
            console.log(res.data);
            llenarTablaInmuebles(res.data);
        })
        .catch(err => console.log(err));    
    }
})





let tablaInmuebles = document.querySelector("#tablaInmuebles");
const llenarTablaInmuebles = (inmuebles) => {
    tablaInmuebles.innerHTML = "";
    let maqueta = "";
    console.log(inmuebles);
    for(let i of inmuebles){
        let status = i.estado? `
            <a href="/Inmueble/Editar/${i.idInmueble}" class="btn btn-editar">Editar</a>
            <form action="/Inmueble/Borrar" method="post" style="display:inline;">
                <input type="hidden" name="id" value="${i.idInmueble}" />
                <input type="submit" value="Eliminar" class="btn btn-eliminar" />
            </form>
            `:`
            <form action="/Inmueble/Alta" method="post">
                <input type="hidden" name="id" value="${i.idInmueble}" />
                <button type="submit" class="btn mx-1" style="background-color: #343a40;" title="Dar de alta">
                    <i class="fa-solid fa-arrow-up" style="color: #44ff00;"></i>
                </button>
            </form>
            `;
        maqueta += `
            <tr>
                <td>${i.tipo.observacion}</td>
                <td>${i.descripcion}</td>
                <td>${i.cantidadAmbientes}</td>
                <td>$${i.precio}</td>
                <td><input disabled class="check-box" type="checkbox" ${i.cochera? 'checked':''}></td>
                <td><input disabled class="check-box" type="checkbox" ${i.piscina? 'checked':''}></td>
                <td><input disabled class="check-box" type="checkbox" ${i.mascotas? 'checked':''}></td>
                <td>
                    ${status}
                </td>
            </tr>`;

    }
    tablaInmuebles.innerHTML = maqueta;
}