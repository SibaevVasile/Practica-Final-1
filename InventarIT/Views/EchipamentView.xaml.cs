using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InventarIT.Data;
using InventarIT.Models;

namespace InventarIT.Views
{
    public partial class EchipamentView : Page
    {
        private readonly DatabaseHelper _db = new();
        private List<Echipament> _toateEchipamentele = new();

        public EchipamentView()
        {
            InitializeComponent();
            IncarcaDate();
        }

        private void IncarcaDate()
        {
            _toateEchipamentele = _db.GetAllEchipamente();
            AplicaFiltru();
        }

        private void AplicaFiltru()
        {
            var producator = txtFiltruProducator.Text
                             .ToLower().Trim();
            var tip = (cmbFiltruTip.SelectedItem
                       as ComboBoxItem)?.Content.ToString();

            var rezultat = _toateEchipamentele.AsEnumerable();

            if (!string.IsNullOrEmpty(producator))
                rezultat = rezultat.Where(e =>
                    e.Producator.ToLower().Contains(producator));

            if (tip != null && tip != "Toate")
                rezultat = rezultat.Where(e => e.Tip == tip);

            dgEchipamente.ItemsSource = rezultat.ToList();
        }

        private void Filtru_Changed(object sender, RoutedEventArgs e)
        {
            AplicaFiltru();
        }

        private void btnReseteazaFiltru_Click(
            object sender, RoutedEventArgs e)
        {
            txtFiltruProducator.Text = string.Empty;
            cmbFiltruTip.SelectedIndex = 0;
        }

        private void btnAdauga_Click(
            object sender, RoutedEventArgs e)
        {
            var fereastra = new EchipamentEditWindow();
            fereastra.Owner = Window.GetWindow(this);
            if (fereastra.ShowDialog() == true)
                IncarcaDate();
        }

        private void btnModifica_Click(
            object sender, RoutedEventArgs e)
        {
            if (dgEchipamente.SelectedItem is not Echipament selectat)
            {
                MessageBox.Show(
                    "Selectați un echipament pentru modificare.",
                    "Atenție", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var fereastra = new EchipamentEditWindow(selectat);
            fereastra.Owner = Window.GetWindow(this);
            if (fereastra.ShowDialog() == true)
                IncarcaDate();
        }

        private void btnSterge_Click(
            object sender, RoutedEventArgs e)
        {
            if (dgEchipamente.SelectedItem is not Echipament selectat)
            {
                MessageBox.Show(
                    "Selectați un echipament pentru ștergere.",
                    "Atenție", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var confirmare = MessageBox.Show(
                $"Sigur doriți să ștergeți echipamentul\n" +
                $"'{selectat.Denumire} ({selectat.NumarSerie})'?",
                "Confirmare ștergere",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirmare == MessageBoxResult.Yes)
            {
                _db.DeleteEchipament(selectat.IdEchipament);
                IncarcaDate();
            }
        }
    }
}