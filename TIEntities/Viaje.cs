namespace TIEntities
{
    public class Viaje
    {
        public int Codigo { get; set; }
        public int PatenteCamionetaAsignada { get; set; }
        public DateOnly FechaPosibleEntregaDesde { get; set; }
        public DateOnly FechaPosibleEntregaHasta { get; set; }
        public double PorcentajeOcupacionCarga { get; set; }
        public List<int> CodigoCompras { get; set; }
    }
}