//Anulacion del contrato
let botonFinalizarContrato = document.querySelector("#botonFinalizarContrato");
let multa = 0;
botonFinalizarContrato.addEventListener("click", () => {
    let IdContrato = document.querySelector("#inputIdContrato").value;
    console.log(IdContrato)
    axios("http://localhost:5203/Pago/GetPorContrato", {params:{IdContrato}})
    .then(res => {
        multa = calcularMulta(res.data)
        Swal.fire({
            title: "Esta seguro?",
            text: `Debera pagar una multa de: $${multa}!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Si, finalizar"
          }).then((result) => {
            if (result.isConfirmed) {
                let fechaAnulacion = new Date();
                let IdContrato = document.querySelector("#inputIdContrato").value;
                console.log(IdContrato);
                console.log(multa);
                console.log(fechaAnulacion);
                axios.post("http://localhost:5203/Contrato/AnularContrato", {IdContrato, multa, fechaAnulacion})
                .then(res => {
                    if(res.data){
                        Swal.fire({
                            title: "Se ha anulado el contrato",
                            icon: "success"
                        }).then(result => window.location.href = "/Contrato/Index");
                    }else{
                        Swal.fire({
                            title: "error al anular contrato",
                            icon: "error"
                        });
                    }
                }).catch(err => console.log(err));
            }
          });
    })
    .catch(err => console.log(err));
})

const calcularMulta = (pagos) => {
    let totalMulta = 0;
    let fechaInicio = new Date(document.querySelector("#fechaInicioContrato").value);
    let fechaFin = new Date(document.querySelector("#fechaFinContrato").value);
    let montoTotalContrato = document.querySelector("#montoTotal").value;
    const anios = fechaFin.getFullYear() - fechaInicio.getFullYear();
    const meses = fechaFin.getMonth() - fechaInicio.getMonth();
    const totalMeses = anios * 12 + meses;
    const pagoMensual = montoTotalContrato / totalMeses;
    let totalYaPagado = 0;
    for(let p of pagos){
        totalYaPagado += p.importe;
    }
    let restaPagar = montoTotalContrato - totalYaPagado;
    let fechaActual = new Date();
    let diasTotales = Math.ceil((fechaFin-fechaInicio) / (1000 * 60 * 60 * 24));
    let diasTranscurridos = Math.ceil((fechaActual-fechaInicio) / (1000 * 60 * 60 * 24));
    if(diasTotales / 2 >= diasTranscurridos){
        totalMulta = pagoMensual + restaPagar;
    }else{
        totalMulta = (pagoMensual * 2) + restaPagar;
    }
    return totalMulta;
}