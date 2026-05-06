using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarIT.Models
{
    public class Echipament
    {
        public int IdEchipament { get; set; }
        public string Denumire { get; set; } = string.Empty;
        public string Tip { get; set; } = string.Empty;
        public string NumarSerie { get; set; } = string.Empty;
        public string Producator { get; set; } = string.Empty;
        public decimal Valoare { get; set; }
        public string Status { get; set; } = "Disponibil";
        public string ValoareFormatata =>
            $"{Valoare:N2} lei";
        public override string ToString() =>
            $"{Denumire} ({NumarSerie})";
    }
}