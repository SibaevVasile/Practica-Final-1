using System.Windows;
using InventarIT.Data;
using InventarIT.Helpers;
using InventarIT.Models;

namespace InventarIT.Views
{
    public partial class AngajatEditWindow : Window
    {
        private readonly DatabaseHelper _db = new();
        private Angajat? _angajatDeModificat;

        // Constructor pentru ADAUGARE
        public AngajatEditWindow()
        {
            InitializeComponent();
            txtTitlu.Text = "Adaugă Angajat";
        }

        // Constructor pentru MODIFICARE
        public AngajatEditWindow(Angajat angajat)
        {
            InitializeComponent();
            _angajatDeModificat = angajat;
            txtTitlu.Text = "Modifică Angajat";
            PreCompleteazaCampuri(angajat);
        }

        private void PreCompleteazaCampuri(Angajat a)
        {
            txtNume.Text = a.Nume;
            txtPrenume.Text = a.Prenume;
            txtEmail.Text = a.Email;
            txtTelefon.Text = a.Telefon;

            // Selecteaza departamentul in ComboBox
            foreach (var item in cmbDepartament.Items)
            {
                if (item is System.Windows.Controls
                    .ComboBoxItem cbi &&
                    cbi.Content.ToString() == a.Departament)
                {
                    cmbDepartament.SelectedItem = cbi;
                    break;
                }
            }
        }

        private void btnSalveaza_Click(
            object sender, RoutedEventArgs e)
        {
            // Validare campuri
            if (!ValidationHelper.ValidareAngajat(
                txtNume.Text,
                txtPrenume.Text,
                cmbDepartament.Text,
                txtEmail.Text))
                return;

            var angajat = new Angajat
            {
                Nume = txtNume.Text.Trim(),
                Prenume = txtPrenume.Text.Trim(),
                Departament = cmbDepartament.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefon = txtTelefon.Text.Trim()
            };

            if (_angajatDeModificat == null)
            {
                // ADAUGARE
                _db.AddAngajat(angajat);
                MessageBox.Show(
                    "Angajatul a fost adăugat cu succes!",
                    "Succes", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                // MODIFICARE
                angajat.IdAngajat = _angajatDeModificat.IdAngajat;
                _db.UpdateAngajat(angajat);
                MessageBox.Show(
                    "Angajatul a fost modificat cu succes!",
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