namespace TIEntities
{
    public class Compra
    {
        public int Codigo { get; set; }
        public int CodigoProducto { get; set; }
        public int DniCliente { get; set; }
        public DateOnly FechaCompra { get; set; }
        public int CantidadComprada { get; set; }
        public DateOnly FechaEntregaSolicitada { get; set; }
        public DateOnly FechaEntregaEstimada { get; set; }
        public EnumEstadoCompra Estado { get; set; }
        public double MontoTotal { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double PrecioProducto { get; set; }


        public Compra()
        {
            FechaCompra = DateOnly.FromDateTime(DateTime.Now);
            Estado = EnumEstadoCompra.OPEN;
        }
        public double CalcularMontoTotal()
        {
            double Monto = (PrecioProducto * CantidadComprada) * 1.21;
            if (CantidadComprada > 4)
            {
                Monto = CalcularPorcentajeDescuento(Monto);
            }
            return Monto;
        }

        public double CalcularPorcentajeDescuento(double Monto)
        {
            return Monto * 0.75;
        }

        public double CalcularDistancia()
        {
            double RadioTierraKm = 6371;
            double longitud1 = -61.49;
            double latitud1 = -31.25;
            // Convertir las latitudes y longitudes de grados a radianes
            double radLatitud1 = GradosARadianes(latitud1);
            double radLongitud1 = GradosARadianes(longitud1);
            double radLatitud2 = GradosARadianes(Latitud);
            double radLongitud2 = GradosARadianes(Longitud);

            // Diferencias entre las coordenadas
            double difLatitudes = radLatitud2 - radLatitud1;
            double difLongitudes = radLongitud2 - radLongitud1;

            // Aplicar la fórmula de Haversine
            double a = Math.Sin(difLatitudes / 2) * Math.Sin(difLatitudes / 2) +
                       Math.Cos(radLatitud1) * Math.Cos(radLatitud2) *
                       Math.Sin(difLongitudes / 2) * Math.Sin(difLongitudes / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Distancia final en kilómetros
            return RadioTierraKm * c;
        }

        // Método para convertir grados a radianes
        public static double GradosARadianes(double grados)
        {
            return grados * (Math.PI / 180);
        }
    }
}