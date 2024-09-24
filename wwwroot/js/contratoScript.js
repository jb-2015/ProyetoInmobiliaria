//filtros
let selectFiltroPorInmueble = document.querySelector("#selectFiltroPorInmueble");
selectFiltroPorInmueble.addEventListener("change", () => {
    let IdInmueble = selectFiltroPorInmueble.selectedOptions[0].value;
    axios("http://localhost:5203/Contrato/GetPorInmueble", {params: {IdInmueble}})
    .then(res => llenarTablaContratos(res.data, IdInmueble))
    .catch(err => console.log(err));
})

//filtro de contratos vigentes entre dos fechas
let botonBuscarVigentes = document.querySelector("#buscarFiltroFechas");
let fechaDesde = document.querySelector("#desdeFiltroContrato");
let fechaHasta = document.querySelector("#hastaFiltroContrato");
botonBuscarVigentes.addEventListener("click", () => {
    if(fechaDesde.value && fechaHasta.value){
        const fechaInicio = new Date(fechaDesde.value).toISOString().split('T')[0];
        const fechaFin = new Date(fechaHasta.value).toISOString().split('T')[0];
        axios("http://localhost:5203/Contrato/GetVigentesPorFechas", {params:{fechaInicio, fechaFin}})
        .then(res => llenarTablaContratos(res.data))
        .catch(err => console.log(err));
    }
})

//filtro de contratos que terminan en 30 dias, 60 dias, 90 dias
let dias30 = document.querySelector("#dias30");
let dias60 = document.querySelector("#dias60");
let dias90 = document.querySelector("#dias90");
let filtroContratosPorDias = document.querySelector(".filtroContratosPorDias");
filtroContratosPorDias.addEventListener("change", (e) => {
    let hoy = new Date();
    let tantosDias = new Date();
    if(e.target.matches("#dias30") || e.target.matches("#dias60") || e.target.matches("#dias90")){
        switch(parseInt(e.target.value)){
            case 1: tantosDias.setDate(tantosDias.getDate() + 30); break;
            case 2: tantosDias.setDate(tantosDias.getDate() + 60); break;
            case 3: tantosDias.setDate(tantosDias.getDate() + 90); break;
        }
        axios("http://localhost:5203/Contrato/GetVigentesDentroDe", {params:{hoy, tantosDias}})
        .then(res => llenarTablaContratos(res.data))
        .catch(err => console.log(err));
    }
})

//validacion de fechas


//metodo llenar tabla 
let tablaContratos = document.querySelector("#tablaContratos");
const llenarTablaContratos = (contratos, IdInmueble) => {
    tablaContratos.innerHTML = "";
    let maqueta = "";
    if(contratos.length > 0){
        for(let c of contratos){
            maqueta += 
            `<tr>
                <td><a href="/Inquilino/Detalle/${c.idInquilino}">${c.inquilino.nombre} ${c.inquilino.apellido}</a></td>
                <td><a href="/Inmueble/Detalles/${c.idInmueble}">${c.inmueble.descripcion}</a></td>
                <td>$${c.monto}</td>
                <td>${(new Date(c.fechaInicio)).toLocaleDateString('en-GB')}</td>
                <td>${(new Date(c.fechaFin)).toLocaleDateString('en-GB')}</td>
                <td>${(c.fechaAnulacion.Millisecond > 0? (new Date(c.fechaAnulacion)).toLocaleDateString('en-GB') : "---")}</td>
                <td><a href="/Pago/Index/${c.IdContrato}" class="btn btn-primary">Pagos</a></td>
            </tr>
            `;
        }
        maqueta += `<a href="/Contrato/Crear/${IdInmueble}" class="btn btn-primary">Nuevo contrato</a>`;
    }else{
        maqueta += 
        `<tr>
            <td>No existen contratos para este inmueble</td>
            <td><a href="/Contrato/Crear/${IdInmueble}" class="btn btn-primary">Nuevo contrato</a></td>
        </tr>`;
    }
    
    tablaContratos.innerHTML = maqueta; 
}