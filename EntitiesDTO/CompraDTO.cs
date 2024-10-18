namespace EntitiesDTO
{
    public class CompraDTO
    {
        public int CodigoProducto { get; set; }
        public int DniCliente { get; set; }
        public int CantidadComprada { get; set; }
        public DateOnly FechaEntregaSolicitada { get; set; }
    }
}