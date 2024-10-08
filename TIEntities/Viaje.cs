namespace TIEntities
{
    public class Viaje
    {
        public int Codigo { get; set; }
        public int PatenteCamionetaAsignada { get; set; }
        public DateOnly FechaDesde { get; set; }
        public DateOnly FechaHasta { get; set; }
        public double PorcentajeOcupacionCarga { get; set; }
        public List<int> CodigoCompras { get; set; }
    }
}