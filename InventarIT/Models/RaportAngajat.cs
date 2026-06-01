namespace InventarIT.Models
{
    public class RaportAngajat
    {
        public int Rand { get; set; }
        public string NumeAngajat { get; set; } = string.Empty;
        public string Departament { get; set; } = string.Empty;
        public int NrEchipamente { get; set; }
        public decimal ValoareTotala { get; set; }
    }

    public class StatisticiInventar
    {
        public int TotalAngajati { get; set; }
        public decimal ValoareTotala { get; set; }
        public decimal MediePerAngajat { get; set; }
        public string CelMaiValoros { get; set; }
                       = string.Empty;
    }
}