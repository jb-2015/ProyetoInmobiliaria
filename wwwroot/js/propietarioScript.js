//Filtrado por dni

let tablaPropietarios = document.querySelector("#tablaPropietarios");
let filtrarPorDni = document.querySelector("#filtrarPorDni");
let buscarPropietarioDNI = document.querySelector("#buscarPropietarioDNI");
buscarPropietarioDNI.addEventListener("input", (e) => {
    if(e.target.value === ""){
        rellenarTablaPropietarios();
    }
})
buscarPropietarioDNI.addEventListener("focus",(e) => {
    if(e.target.value === ""){
        buscarPropietarioApellido.value = "";
        switchBoton();
        buscarPropietarioEmail.value = "";
        rellenarTablaPropietarios();
    }
})
filtrarPorDni.addEventListener("click", (e) => {
    let dni = buscarPropietarioDNI.value;
    axios.get("http://localhost:5203/Propietario/GetPropietariosPorDni", {params:{dni: dni}})
    .then(res => {
        let Propietarios = res.data;
        llenarTablaPropietarios(tablaPropietarios, Propietarios);
    })
    .catch(err => console.log(err));
})



//Filtrado por apellido
let buscarPropietarioApellido = document.querySelector("#buscarPropietarioApellido");
buscarPropietarioApellido.addEventListener("input", (e) => {
    if(e.target.value === ""){
        rellenarTablaPropietarios();
    }
})
buscarPropietarioApellido.addEventListener("focus", (e) => {
    if(e.target.value === ""){
        buscarPropietarioDNI.value = "";
        switchBoton();
        buscarPropietarioEmail.value = "";
        rellenarTablaPropietarios();
    }
})


let filtrarPorApellido = document.querySelector("#filtrarPorApellido");
filtrarPorApellido.addEventListener("click", () => {
    let Apellido = buscarPropietarioApellido.value;
    axios("http://localhost:5203/Propietario/GetPropietariosPorApellido", {params: {Apellido: Apellido}})
    .then(res => {
        let elementos = res.data;
        llenarTablaPropietarios(tablaPropietarios, elementos);
    })
    .catch(err => console.log(err));
}) 

//Filtrado por email
let buscarPropietarioEmail = document.querySelector("#buscarPropietarioEmail");
buscarPropietarioEmail.addEventListener("input", (e) => {
    if(e.target.value === ""){
        rellenarTablaPropietarios();
    }
})
buscarPropietarioEmail.addEventListener("focus",(e) => {
    if(e.target.value === ""){
        buscarPropietarioDNI.value = "";
        switchBoton();
        buscarPropietarioApellido.value = "";
        rellenarTablaPropietarios();
    }
})
let filtrarPorEmail = document.querySelector("#filtrarPorEmail");
filtrarPorEmail.addEventListener("click", () => {
    let Email = buscarPropietarioEmail.value;
    axios("http://localhost:5203/Propietario/GetPropietariosPorEmail", {params: {Email: Email}})
    .then(res => {
        let elementos = res.data;
        llenarTablaPropietarios(tablaPropietarios, elementos);
    })
    .catch(err => console.log(err));
}) 

const rellenarTablaPropietarios = () => {
    axios("http://localhost:5203/Propietario/GetPropietarios")
    .then(res => {
        llenarTablaPropietarios(tablaPropietarios, res.data);
    })
    .catch(err => console.log(err));
}

const llenarTablaPropietarios = (tabla, elementos) => {
    tabla.innerHTML = "";
    let maqueta = "";
    for(let e of elementos){
        maqueta += `<tr>
                <td>${e.dni}</td>
                <td>${e.nombre}</td>
                <td>${e.apellido}</td>
                <td>${e.telefono}</td>
                <td>${e.correo}</td>
                <td class="d-flex align-items-center w-auto">
                    <a class="btn btn-editbt mx-1" href="/Propietario/Editar/${e.idPropietario}">
                        <i class="fa-solid fa-pen-to-square"></i>
                    </a>
                    <form action="/Propietario/Borrar" class="mx-1" method="post">
                        <input type="hidden" name="id" value="${e.idPropietario}" />
                        <button type="submit" class="btn btn-borrarbt" title="Borrar">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    </form> 
                    <a class="btn btn-detallebt mx-1" href="/Propietario/Detalle/${e.idPropietario}">
                        <i class="fa-solid fa-list"></i>
                    </a>
                </td>
            </tr>`
    }
    tabla.innerHTML = maqueta;
}

//Filtrar por dados de baja
const switchBoton = () => {
    if(botonDadosDeBaja.classList.contains("btn-success")){
        botonDadosDeBaja.classList.remove("btn-success");
        botonDadosDeBaja.classList.add("btn-danger");
        botonDadosDeBaja.innerHTML = "Dados de baja";
    }
}


let botonDadosDeBaja = document.querySelector("#propietariosDadosDeBaja");
botonDadosDeBaja.addEventListener("click", () => {
    if(botonDadosDeBaja.classList.contains("btn-danger")){
        botonDadosDeBaja.classList.remove("btn-danger");
        botonDadosDeBaja.classList.add("btn-success");
        botonDadosDeBaja.innerHTML = "Dados de alta";
        axios("http://localhost:5203/Propietario/DadosDeBaja")
        .then(res => {
            tablaPropietarios.innerHTML = "";
            maqueta = "";
            for(let i of res.data){
                maqueta += `<tr>
                    <td>${i.dni}</td>
                    <td>${i.nombre}</td>
                    <td>${i.apellido}</td>
                    <td>${i.telefono}</td>
                    <td>${i.correo}</td>
                    <td>
                        <form action="/Propietario/Alta/${i.idPropietario}" method="post">
                        <button type="submit" class="btn mx-1" style="background-color: #343a40;" title="Dar de alta">
                            <i class="fa-solid fa-arrow-up" style="color: #44ff00;"></i>
                        </button>    
                    </td>
                </tr>`
            }
            tablaPropietarios.innerHTML = maqueta;
        }).catch(err => console.log(err));
    }else{
        botonDadosDeBaja.classList.remove("btn-success");
        botonDadosDeBaja.classList.add("btn-danger");
        botonDadosDeBaja.innerHTML = "Dados de baja";
        rellenarTablaPropietarios();
    }
})