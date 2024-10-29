namespace TIEntities
{
    public class Compra
    {
        public int Codigo { get; set; }
        public int CodigoProducto { get; set; }
        public int DniCliente { get; set; }
        public DateTime FechaCompra { get; set; }
        public int CantidadComprada { get; set; }
        public DateTime FechaEntregaSolicitada { get; set; }
        public DateTime FechaEntregaEstimada { get; set; }
        public EnumEstadoCompra Estado { get; set; }
        public double MontoTotal { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double PrecioProducto { get; set; }

        public Compra()
        {
            FechaCompra = DateTime.Now;
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
            double longitud1 = -61.4867;
            double latitud1 = -31.25033;
            double radLatitud1 = GradosARadianes(latitud1);
            double radLongitud1 = GradosARadianes(longitud1);
            double radLatitud2 = GradosARadianes(Latitud);
            double radLongitud2 = GradosARadianes(Longitud);
            double difLatitudes = radLatitud2 - radLatitud1;
            double difLongitudes = radLongitud2 - radLongitud1;
            double a = Math.Sin(difLatitudes / 2) * Math.Sin(difLatitudes / 2) +
                       Math.Cos(radLatitud1) * Math.Cos(radLatitud2) *
                       Math.Sin(difLongitudes / 2) * Math.Sin(difLongitudes / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return RadioTierraKm * c;
        }

        public static double GradosARadianes(double grados)
        {
            return grados * (Math.PI / 180);
        }
    }
}