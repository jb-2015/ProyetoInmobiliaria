let selectUsuariosAuditoria = document.querySelector("#selectUsuariosAuditoria");
selectUsuariosAuditoria.addEventListener("change", () => {
    let id = selectUsuariosAuditoria.selectedOptions[0].value;
    axios("http://localhost:5203/Auditoria/GetPorUsuario", {params:{id}})
    .then(res => llenarTablaAuditoria(res.data))
    .catch(err => console.log(err))
})

const llenarTablaAuditoria = (auditorias) => {
    let tablaAuditorias = document.querySelector("#tablaAuditorias");
    tablaAuditorias.innerHTML = "";
    let maqueta = "";
    console.log(auditorias);
    if(auditorias.length > 0){
        for(let a of auditorias){
            maqueta += 
                `<tr>
                    <td>${a.idAuditoria}</td>
                    <td>${a.usuario.idUsuario}: ${a.usuario.apellido} ${a.usuario.nombre}</td>
                    <td>${a.accion}</td>
                    <td>${a.observacion}</td>
                    <td>${a.fechaYHora}</td>
                </tr>`;
        }
    }else{
        maqueta += 
                `<tr>
                    <td>----------------------</td>
                    <td>----------------------</td>
                    <td>No existen datos todavía</td>
                    <td>----------------------</td>
                    <td>----------------------</td>
                </tr>`;
    }
    tablaAuditorias.innerHTML = maqueta;
}