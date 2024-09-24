let selectTipos = document.querySelector("#selectTipos");
let fechaDesdeInmuebles = document.querySelector("#fechaDesdeInmuebles");
let fechaHastaInmuebles = document.querySelector("#fechaHastaInmuebles");
let btnFiltrarInmueblesIndex = document.querySelector("#btnFiltrarInmueblesIndex");
btnFiltrarInmueblesIndex.addEventListener("click", () => {
    if(selectTipos.selectedOptions[0].value != 0 && fechaDesdeInmuebles.value && fechaHastaInmuebles.value){
        let idTipo = selectTipos.selectedOptions[0].value;
        let fechaDesde = fechaDesdeInmuebles.value;
        let fechaHasta = fechaHastaInmuebles.value;
        axios("http://localhost:5203/Home/GetInmueblesPorTipoYFechas", {params:{idTipo, fechaDesde, fechaHasta}})
        .then(res => llenarCartasInmuebles(res.data))
        .catch(err => console.log(err));
    }
})
let btnLimpiarFiltros = document.querySelector("#limpiarFiltros");
btnLimpiarFiltros.addEventListener("click", () => {
    axios("http://localhost:5203/Home/GetTodos")
    .then(res => llenarCartasInmuebles(res.data))
    .catch(err => console.log(err));
})

const llenarCartasInmuebles = (inmuebles) => {
    let containerInmueble = document.querySelector(".containerInmueble");
    containerInmueble.innerHTML = "";
    let maqueta = "";
    for(let i of inmuebles){
        maqueta +=         
        `<a href="/Inmueble/Detalles/${i.idInmueble}">
            <div class="cardInmueble">
                <img src="${i.urlImagen}" alt="">
                <h2>${i.tipo.observacion}</h2>
                <p>${i.descripcion}</p>
            </div>
        </a>`;
    }
    containerInmueble.innerHTML = maqueta;
}