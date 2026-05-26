using System;
using System.Windows;
using System.Windows.Controls;
using InventarIT.Data;

namespace InventarIT.Views
{
    public partial class AtribuireEditWindow : Window
    {
        private readonly DatabaseHelper _db = new();

        public AtribuireEditWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => Initializeaza();
        }

        private void Initializeaza()
        {
            txtDataAtribuire.Text =
                DateTime.Today.ToString("dd.MM.yyyy");

            cmbAngajat.ItemsSource = _db.GetAllAngajati();

            cmbEchipament.ItemsSource =
                _db.GetEchipamenteDisponibile();
        }

        private void BtnSalveaza_Click(
            object sender, RoutedEventArgs e)
        {
            if (cmbAngajat.SelectedItem == null)
            {
                MessageBox.Show(
                    "Selectați un angajat.",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (cmbEchipament.SelectedItem == null)
            {
                MessageBox.Show(
                    "Selectați un echipament disponibil.",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            int idAngajat = (int)cmbAngajat.SelectedValue;
            int idEchipament = (int)cmbEchipament.SelectedValue;
            var status = (cmbStatus.SelectedItem
                                as ComboBoxItem)
                               ?.Content.ToString() ?? "Activa";
            var observatii = txtObservatii.Text.Trim();

            if (_db.ExistaAtribuireActiva(idEchipament))
            {
                MessageBox.Show(
                    "Echipamentul selectat este deja atribuit!\n" +
                    "Returnați echipamentul înainte de " +
                    "o nouă atribuire.",
                    "Atribuire imposibilă",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            _db.AddAtribuire(
                idEchipament, idAngajat,
                DateTime.Today, status, observatii);

            MessageBox.Show(
                "Echipamentul a fost atribuit cu succes!",
                "Succes", MessageBoxButton.OK,
                MessageBoxImage.Information);

            DialogResult = true;
            Close();
        }

        private void BtnAnuleaza_Click(
            object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}