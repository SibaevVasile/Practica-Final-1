using System.Windows;
using System.Windows.Controls;
using InventarIT.Data;
using InventarIT.Helpers;
using InventarIT.Models;

namespace InventarIT.Views
{
    public partial class EchipamentEditWindow : Window
    {
        private readonly DatabaseHelper _db = new();
        private Echipament _echipamentDeModificat = null!;
        public EchipamentEditWindow()
        {
            InitializeComponent();
            txtTitlu.Text = "Adaugă Echipament";
        }
        public EchipamentEditWindow(Echipament echipament)
        {
            InitializeComponent();
            _echipamentDeModificat = echipament;
            txtTitlu.Text = "Modifică Echipament";
            PreCompleteazaCampuri(echipament);
        }

        private void PreCompleteazaCampuri(Echipament e)
        {
            txtDenumire.Text = e.Denumire;
            txtNumarSerie.Text = e.NumarSerie;
            txtProducator.Text = e.Producator;
            txtValoare.Text = e.Valoare.ToString("F2");
            foreach (ComboBoxItem item in cmbTip.Items)
                if (item.Content.ToString() == e.Tip)
                { cmbTip.SelectedItem = item; break; }

            foreach (ComboBoxItem item in cmbStatus.Items)
                if (item.Content.ToString() == e.Status)
                { cmbStatus.SelectedItem = item; break; }
        }

        private void btnSalveaza_Click(
            object sender, RoutedEventArgs e)
        {

            if (!ValidationHelper.ValidareEchipament(
                txtDenumire.Text,
                cmbTip.Text,
                txtNumarSerie.Text,
                txtProducator.Text,
                txtValoare.Text))
                return;

            decimal valoare = decimal.Parse(txtValoare.Text
                .Replace(",", "."),
                System.Globalization.CultureInfo.InvariantCulture);

            var echipament = new Echipament
            {
                Denumire = txtDenumire.Text.Trim(),
                Tip = cmbTip.Text.Trim(),
                NumarSerie = txtNumarSerie.Text.Trim(),
                Producator = txtProducator.Text.Trim(),
                Valoare = valoare,
                Status = cmbStatus.Text.Trim()
            };

            if (_echipamentDeModificat == null!)
            {
                _db.AddEchipament(echipament);
                MessageBox.Show(
                    "Echipamentul a fost adăugat cu succes!",
                    "Succes", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                echipament.IdEchipament =
                    _echipamentDeModificat.IdEchipament;
                _db.UpdateEchipament(echipament);
                MessageBox.Show(
                    "Echipamentul a fost modificat cu succes!",
                    "Succes", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            DialogResult = true;
            Close();
        }

        private void btnAnuleaza_Click(
            object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}