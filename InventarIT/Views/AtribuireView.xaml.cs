using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InventarIT.Data;
using InventarIT.Models;

namespace InventarIT.Views
{
    public partial class AtribuireView : Page
    {
        private readonly DatabaseHelper _db = new();
        private List<Atribuire> _toateAtribuirile = new();

        public AtribuireView()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                IncarcaFiltruAngajati();
                IncarcaDate();
            };
        }

        private void IncarcaFiltruAngajati()
        {
            var angajati = _db.GetAllAngajati();
            cmbFiltruAngajat.Items.Clear();
            cmbFiltruAngajat.Items.Add(
                new ComboBoxItem
                {
                    Content = "Toți",
                    IsSelected = true
                });
            foreach (var a in angajati)
                cmbFiltruAngajat.Items.Add(
                    new ComboBoxItem
                    {
                        Content = a.NumeComplet,
                        Tag = a.IdAngajat
                    });
            cmbFiltruAngajat.SelectedIndex = 0;
        }

        private void IncarcaDate()
        {
            _toateAtribuirile = _db.GetAllAtribuiri();
            AplicaFiltru();
        }

        private void AplicaFiltru()
        {
            if (dgAtribuiri == null)
                return;

            var angajatItem = cmbFiltruAngajat.SelectedItem
                              as ComboBoxItem;
            var statusItem = cmbFiltruStatus.SelectedItem
                              as ComboBoxItem;

            var rezultat = _toateAtribuirile.AsEnumerable();

            if (angajatItem?.Tag is int idAngajat)
                rezultat = rezultat.Where(
                    a => a.IdAngajat == idAngajat);

            var status = statusItem?.Content.ToString();
            if (status != null && status != "Toate")
                rezultat = rezultat.Where(a =>
                    a.StatusAtribuire == status);

            dgAtribuiri.ItemsSource = rezultat.ToList();
        }

        private void Filtru_Changed(object sender, RoutedEventArgs e)
            => AplicaFiltru();

        private void BtnReseteazaFiltru_Click(
            object sender, RoutedEventArgs e)
        {
            cmbFiltruAngajat.SelectedIndex = 0;
            cmbFiltruStatus.SelectedIndex = 0;
        }

        private void BtnAtribuie_Click(
            object sender, RoutedEventArgs e)
        {
            var fereastra = new AtribuireEditWindow();
            fereastra.Owner = Window.GetWindow(this);
            if (fereastra.ShowDialog() == true)
                IncarcaDate();
        }

        private void BtnReturneaza_Click(
            object sender, RoutedEventArgs e)
        {
            if (dgAtribuiri.SelectedItem is not Atribuire selectata)
            {
                MessageBox.Show(
                    "Selectați o atribuire pentru returnare.",
                    "Atenție", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (!selectata.EsteActiva)
            {
                MessageBox.Show(
                    "Această atribuire este deja returnată.",
                    "Atenție", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var confirmare = MessageBox.Show(
                $"Confirmați returnarea echipamentului\n" +
                $"'{selectata.DenumireEchipament}'\n" +
                $"de la '{selectata.NumeAngajat}'?",
                "Confirmare returnare",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirmare == MessageBoxResult.Yes)
            {
                _db.ReturneazaEchipament(selectata.IdAtribuire);
                IncarcaDate();
            }
        }

        private void BtnAnuleaza_Click(
            object sender, RoutedEventArgs e)
        {
            if (dgAtribuiri.SelectedItem is not Atribuire selectata)
            {
                MessageBox.Show(
                    "Selectați o atribuire pentru anulare.",
                    "Atenție", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var confirmare = MessageBox.Show(
                $"Sigur doriți să anulați atribuirea\n" +
                $"'{selectata.DenumireEchipament}' → " +
                $"'{selectata.NumeAngajat}'?",
                "Confirmare anulare",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirmare == MessageBoxResult.Yes)
            {
                _db.DeleteAtribuire(selectata.IdAtribuire);
                IncarcaDate();
            }
        }
    }
}