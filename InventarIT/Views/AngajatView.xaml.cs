using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InventarIT.Data;
using InventarIT.Models;

namespace InventarIT.Views
{
    public partial class AngajatView : Page
    {
        private readonly DatabaseHelper _db = new();
        private List<Angajat> _totiAngajatii = new();

        public AngajatView()
        {
            InitializeComponent();
            IncarcaDate();
        }

        private void IncarcaDate()
        {
            _totiAngajatii = _db.GetAllAngajati();
            dgAngajati.ItemsSource = _totiAngajatii;
        }

        private void txtCautare_TextChanged(
            object sender, TextChangedEventArgs e)
        {
            var termen = txtCautare.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(termen))
            {
                dgAngajati.ItemsSource = _totiAngajatii;
            }
            else
            {
                dgAngajati.ItemsSource = _totiAngajatii
                    .Where(a =>
                        a.Nume.ToLower().Contains(termen) ||
                        a.Email.ToLower().Contains(termen))
                    .ToList();
            }
        }
        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            var fereastra = new AngajatEditWindow();
            fereastra.Owner = Window.GetWindow(this);
            if (fereastra.ShowDialog() == true)
                IncarcaDate();
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            if (dgAngajati.SelectedItem is not Angajat selectat)
            {
                MessageBox.Show(
                    "Selectați un angajat din listă pentru modificare.",
                    "Atenție", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var fereastra = new AngajatEditWindow(selectat);
            fereastra.Owner = Window.GetWindow(this);
            if (fereastra.ShowDialog() == true)
                IncarcaDate();
        }

        private void btnSterge_Click(object sender, RoutedEventArgs e)
        {
            if (dgAngajati.SelectedItem is not Angajat selectat)
            {
                MessageBox.Show(
                    "Selectați un angajat din listă pentru ștergere.",
                    "Atenție", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var confirmare = MessageBox.Show(
                $"Sigur doriți să ștergeți angajatul\n" +
                $"'{selectat.NumeComplet}'?",
                "Confirmare ștergere",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirmare == MessageBoxResult.Yes)
            {
                _db.DeleteAngajat(selectat.IdAngajat);
                IncarcaDate();
            }
        }
    }
}