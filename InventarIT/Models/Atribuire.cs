using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarIT.Models
{
    public class Atribuire
    {
        public int IdAtribuire { get; set; }
        public int IdEchipament { get; set; }
        public int IdAngajat { get; set; }
        public DateTime DataAtribuire { get; set; }
        public DateTime? DataReturnare { get; set; }
        public string Observatii { get; set; } = string.Empty;

        public string NumeAngajat { get; set; } = string.Empty;
        public string DenumireEchipament { get; set; } = string.Empty;
        public string NumarSerie { get; set; } = string.Empty;
        public decimal ValoareEchipament { get; set; }

        public bool EsteActiva => DataReturnare == null;
        public string StatusAtribuire =>
            EsteActiva ? "Activa" : "Returnata";
    }
}
