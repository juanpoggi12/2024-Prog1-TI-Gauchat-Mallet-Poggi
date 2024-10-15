document.addEventListener("DOMContentLoaded", function () {

    const formulario = document.getElementById("ActualizarStockForm");

    formulario.addEventListener("submit", function (event) {
        event.preventDefault();

        const codigo = document.getElementById("Codigo").value;
        const cantidadAActualizar = document.getElementById("CantidadAActualizar").value;

        const stock = {
            Codigo: codigo,
            CantidadAActualizar: cantidadAActualizar
        };

        let url = `http://localhost:5021/Producto/${codigo}?CantidadAActualizar=${cantidadAActualizar}`;
        fetch(url, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(stock)
        })
            .then(response => {
                if (!response.ok) {
                    return response.text().then(text => {
                        throw new Error(`Error al actualizar el producto: ${text}`);
                    });
                }
                return response.text().then(text => text ? JSON.parse(text) : {});
            })
            .then(data => {
             
                document.getElementById("responseMessage").innerText = "";
                document.getElementById("successGif").style.display = "block";
            })
            .catch(error => {
                
                document.getElementById("responseMessage").innerText = "Hubo un error al actualizar el stock: " + error.message;
                document.getElementById("successGif").style.display = "none";
            });
    });
});
