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
            // Implementat la Issue #15
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            // Implementat la Issue #15
        }

        private void btnSterge_Click(object sender, RoutedEventArgs e)
        {
            // Implementat la Issue #16
        }
    }
}