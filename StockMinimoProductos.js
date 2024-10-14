document.addEventListener("DOMContentLoaded", function () {
    fetch("http://localhost:5021/Producto")
        .then(response => {
            if (!response.ok) {
                throw new Error("Error al obtener los productos");
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


