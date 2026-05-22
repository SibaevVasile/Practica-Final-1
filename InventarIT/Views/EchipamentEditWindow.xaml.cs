using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using InventarIT.Data;
using InventarIT.Helpers;
using InventarIT.Models;

#nullable enable

namespace InventarIT.Views
{
    public partial class EchipamentEditWindow : Window
    {
        private readonly DatabaseHelper _db = new();
        private readonly Echipament? _echipament;

        public EchipamentEditWindow()
        {
            InitializeComponent();
            txtTitlu.Text = "Adaugă Echipament";
            cmbStatus.SelectedIndex = 0;
        }
        public EchipamentEditWindow(Echipament echipament)
        {
            InitializeComponent();
            _echipament = echipament;
            txtTitlu.Text = "Modifică Echipament";
            Loaded += (s, e) => PreCompleteazaCampuri(echipament);
        }

        private void PreCompleteazaCampuri(Echipament e)
        {
            txtDenumire.Text = e.Denumire;
            txtNumarSerie.Text = e.NumarSerie;
            txtProducator.Text = e.Producator;
            txtValoare.Text = e.Valoare.ToString("F2",
                                 CultureInfo.InvariantCulture);

            foreach (ComboBoxItem item in cmbTip.Items)
                if (item.Content.ToString() == e.Tip)
                { cmbTip.SelectedItem = item; break; }

            foreach (ComboBoxItem item in cmbStatus.Items)
                if (item.Content.ToString() == e.Status)
                { cmbStatus.SelectedItem = item; break; }
        }

        private void BtnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            var tip = (cmbTip.SelectedItem as ComboBoxItem)
                         ?.Content.ToString() ?? string.Empty;
            var status = (cmbStatus.SelectedItem as ComboBoxItem)
                         ?.Content.ToString() ?? "Disponibil";

            if (!ValidationHelper.ValidareEchipament(
                    txtDenumire.Text, tip,
                    txtNumarSerie.Text, txtProducator.Text,
                    txtValoare.Text))
                return;

            decimal valoare = decimal.Parse(
                txtValoare.Text.Replace(",", "."),
                CultureInfo.InvariantCulture);

            var echipament = new Echipament
            {
                Denumire = txtDenumire.Text.Trim(),
                Tip = tip,
                NumarSerie = txtNumarSerie.Text.Trim(),
                Producator = txtProducator.Text.Trim(),
                Valoare = valoare,
                Status = status
            };

            if (_echipament == null)
            {
                _db.AddEchipament(echipament);
                MessageBox.Show(
                    "Echipamentul a fost adăugat cu succes!",
                    "Succes", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                echipament.IdEchipament = _echipament.IdEchipament;
                _db.UpdateEchipament(echipament);
                MessageBox.Show(
                    "Echipamentul a fost modificat cu succes!",
                    "Succes", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            DialogResult = true;
            Close();
        }

        private void BtnAnuleaza_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}