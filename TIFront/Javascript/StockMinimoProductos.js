document.addEventListener("DOMContentLoaded", function () {
    fetch("http://localhost:5021/Producto")
        .then(response => {
            if (!response.ok) {
                if (response.status === 404) {
                    document.getElementById("mensajeError").innerText = "No hay productos bajos de stock.";
                    throw new Error("No hay productos bajos de stock");
                } else {
                    throw new Error("Error al obtener los productos");
                }
            }
            return response.json();
        })
        .then(data => {
            const tableBody = document.getElementById("ProductosBajos").getElementsByTagName("tbody")[0];
            data.forEach(producto => {
                const row = tableBody.insertRow();
                row.insertCell(0).innerText = producto.nombre;
                row.insertCell(1).innerText = producto.stock;
                row.insertCell(2).innerText = producto.stockMinimo;
            });
        })
        .catch(error => {
            console.error("Error:", error);
        });
});


