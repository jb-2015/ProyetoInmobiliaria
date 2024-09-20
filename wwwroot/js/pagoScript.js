let botonNuevoPago = document.querySelector("#botonNuevoPago");
let contratoSelect = document.querySelector("#contratos");
botonNuevoPago.addEventListener("click", () => {
    let idContratoSeleccionado = contratoSelect.selectedOptions[0].id;
    window.location.href = `http://localhost:5203/Pago/Crear/${idContratoSeleccionado}`;
})
contratoSelect.addEventListener("change", () => {
    let idContrato = contratoSelect.selectedOptions[0].id;
    console.log(idContrato);
    llenarTabla(idContrato);
})
let tablaPagos = document.querySelector("#tablaPagos");
const llenarTabla = (idContrato) => {
    axios(`http://localhost:5203/Pago/GetPorContrato/`, {params:{idContrato}})
    .then(res => {  
        tablaPagos.innerHTML = "";
        let maqueta = "";
        for(let p of res.data){
            let estado = "";
            let acciones = `
                    <td class="d-flex align-items-center w-auto">
                        <form action="/Pago/Borrar" class="mx-1" method="post">
                            <input type="hidden" name="Id" value="${p.idPago}">
                            <button type="submit" class="btn btn-borrarbt">Anular</button>
                        </form>
                        <a href="/Pago/Editar/${p.idPago}" class="btn btn-editbt mx-1">
                            <i class="fa-solid fa-pen-to-square"></i>
                        </a>
                    </td>`;
            if(p.estado){
                estado = `<td>Pagado</td>${acciones}`;
            }else{
                estado = `<td>Anulado</td>`;
            }
            maqueta += `
            <tr>
                <td>${p.contrato.inmueble.propietario.apellido} - ${p.contrato.inquilino.apellido}</td>
                <td>${(new Date(p.fechaPago)).toLocaleDateString('en-GB')}</td>
                <td>$${p.importe}</td>
                <td>${p.numeroPago}</td>
                <td>${p.detalle}</td>
                ${estado}
            </tr>`;
        }
        tablaPagos.innerHTML = maqueta;
    }).catch(err => console.log(err));
}
