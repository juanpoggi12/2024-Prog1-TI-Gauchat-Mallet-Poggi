namespace TIEntities
{
    public class Viaje
    {
        public int Codigo { get; set; }
        public string PatenteCamionetaAsignada { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public double PorcentajeOcupacionCarga { get; set; }
        public List<int> Compras { get; set; }

        public Viaje()
        {
            Compras = new List<int>();
        }
    }
}