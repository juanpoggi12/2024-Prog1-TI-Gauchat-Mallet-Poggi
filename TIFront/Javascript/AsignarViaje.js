document.addEventListener("DOMContentLoaded", function () {

    const Formulario = document.getElementById("NuevoViaje")


    Formulario.addEventListener("submit", function (event) {
        event.preventDefault();

        const fechadesde = document.getElementById("FechaDesde").value;
        const fechahasta = document.getElementById("FechaHasta").value;
        const Data = {
            FechaDesde: fechadesde,
            FechaHasta: fechahasta
        }
        fetch('http://localhost:5021/Viaje', {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(Data)
        })
            .then(response => {
                if (!response.ok) {
                    return response.text().then(text => {
                        throw new Error(`Error al dar alta: ${text}`);
                    });
                };
            })
            .then(data => {
                console.log("Exito");
                alert("Viaje asignado con exito");
            })
            .catch(error => {
                document.getElementById("Respuesta").innerText = "Hubo un error;" + error.message;
            });
    });
});
