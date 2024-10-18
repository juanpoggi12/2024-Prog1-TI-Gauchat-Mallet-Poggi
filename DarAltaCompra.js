document.addEventListener("DOMContentLoaded", function () {

    const Formulario = document.getElementById("AltaCompra")

    Formulario.addEventListener("submit", function (event) {
        event.preventDefault();

        const codigoproducto = document.getElementById("CodigoProducto").value;
        const dnicliente = document.getElementById("DniCliente").value;
        const cantidadcomprada = document.getElementById("CantidadComprada").value;
        const fechaentregasolicitada = document.getElementById("FechaEntregaSolicitada").value;

        const AltaCompra = {
            CodigoProducto: codigoproducto,
            DniCliente: dnicliente,
            CantidadComprada: cantidadcomprada,
            FechaEntregaSolicitada: fechaentregasolicitada
        }

        fetch("http://localhost:5021/Compra", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(AltaCompra)
        })
            .then(response => {
                if (!response.ok) {
                    return response.text().then(text => {
                        throw new Error( `Error al dar alta: ${text}` );
                    });
                };
            })
            .then(data => {
                console.log("Exito");
                alert("Compra realizada con exito");
            })
            .catch(error => {
                document.getElementById("Respuesta").innerText = "Hubo un error;" + error.message;
            });
    });
});
