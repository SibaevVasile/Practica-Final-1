using System.Windows;
using System.Windows.Controls;
using InventarIT.Data;
using InventarIT.Helpers;
using InventarIT.Models;

#nullable enable

namespace InventarIT.Views
{
    public partial class AngajatEditWindow : Window
    {
        private readonly DatabaseHelper _db = new();
        private readonly Angajat? _angajat;

        public AngajatEditWindow()
        {
            InitializeComponent();
            txtTitlu.Text = "Adaugă Angajat";
        }

        public AngajatEditWindow(Angajat angajat)
        {
            InitializeComponent();
            _angajat = angajat;
            txtTitlu.Text = "Modifică Angajat";
            Loaded += (s, e) => PreCompleteazaCampuri(angajat);
        }

        private void PreCompleteazaCampuri(Angajat a)
        {
            txtNume.Text = a.Nume;
            txtPrenume.Text = a.Prenume;
            txtEmail.Text = a.Email;
            txtTelefon.Text = a.Telefon;

            foreach (ComboBoxItem item in cmbDepartament.Items)
            {
                if (item.Content.ToString() == a.Departament)
                {
                    cmbDepartament.SelectedItem = item;
                    break;
                }
            }
        }

        private void BtnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            var dept = (cmbDepartament.SelectedItem as ComboBoxItem)
                       ?.Content.ToString() ?? string.Empty;

            if (!ValidationHelper.ValidareAngajat(
                    txtNume.Text, txtPrenume.Text,
                    dept, txtEmail.Text))
                return;

            var angajat = new Angajat
            {
                Nume = txtNume.Text.Trim(),
                Prenume = txtPrenume.Text.Trim(),
                Departament = dept,
                Email = txtEmail.Text.Trim(),
                Telefon = txtTelefon.Text.Trim()
            };

            if (_angajat == null)
            {
                _db.AddAngajat(angajat);
                MessageBox.Show("Angajatul a fost adăugat cu succes!",
                    "Succes", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                angajat.IdAngajat = _angajat.IdAngajat;
                _db.UpdateAngajat(angajat);
                MessageBox.Show("Angajatul a fost modificat cu succes!",
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