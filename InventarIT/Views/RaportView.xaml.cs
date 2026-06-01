using System.Windows;
using InventarIT.Data;

namespace InventarIT.Views
{
    public partial class RaportView : Window
    {
        private readonly DatabaseHelper _db = new();

        public RaportView()
        {
            InitializeComponent();
            Loaded += (s, e) => IncarcaRaport();
        }

        private void IncarcaRaport()
        {
            var date = _db.GetRaportAngajati();
            dgRaport.ItemsSource = date;

            // Statistici
            var stats = _db.GetStatisticiInventar();
            txtTotalAngajati.Text =
                stats.TotalAngajati.ToString();
            txtValoareTotala.Text =
                $"{stats.ValoareTotala:N2} lei";
            txtMediePerAngajat.Text =
                $"{stats.MediePerAngajat:N2} lei";
            txtCelMaiValoros.Text =
                stats.CelMaiValoros;
        }

        private void BtnReincarca_Click(
            object sender, RoutedEventArgs e)
            => IncarcaRaport();

        private void BtnExportaTxt_Click(
            object sender, RoutedEventArgs e)
        {
            // Implementat la Issue #27
        }

        private void BtnInchide_Click(
            object sender, RoutedEventArgs e)
            => Close();
    }
}