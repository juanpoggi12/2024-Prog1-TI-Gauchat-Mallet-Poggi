namespace TIEntities
{
    public class Viaje
    {
        public int Codigo { get; set; }
        public string PatenteCamionetaAsignada { get; set; }
        public DateOnly FechaDesde { get; set; }
        public DateOnly FechaHasta { get; set; }
        public double PorcentajeOcupacionCarga { get; set; }
        public List<int> Compras { get; set; }

        public Viaje()
        {
            Compras = new List<int>();
        }
    }
}