namespace TIEntities
{
    public class Producto
    {
//        Código único(Asignado por el sistema)
//Nombre
//Marca
//Alto de la caja(en cm)
//Ancho de la caja(en cm)
//Profundidad de la caja(en cm)
//Precio unitario
//Stock mínimo
//Cantidad en stock 

        public int Codigo { get; set; }
        public string Nombre {  get; set; }
        public string Marca {  get; set; }
        public double AltoCaja {  get; set; }
        public double AnchoCaja {  get; set; }
        public double ProfundidadCaja {  get; set; }
        public double PrecioUnitario {  get; set; }
        public int StockMinimo {  get; set; }
        public int Stock {  get; set; }
    } 
}
