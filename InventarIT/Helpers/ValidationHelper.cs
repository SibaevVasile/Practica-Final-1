using System.Windows;

namespace InventarIT.Helpers
{
    public static class ValidationHelper
    {
        public static bool ValidareAngajat(
            string nume,
            string prenume,
            string departament,
            string email)
        {
            if (string.IsNullOrWhiteSpace(nume))
            {
                MessageBox.Show(
                    "Câmpul 'Nume' este obligatoriu.",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(prenume))
            {
                MessageBox.Show(
                    "Câmpul 'Prenume' este obligatoriu.",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(departament))
            {
                MessageBox.Show(
                    "Vă rugăm selectați un 'Departament'.",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show(
                    "Câmpul 'Email' este obligatoriu.",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            if (!ValidareEmail(email))
            {
                MessageBox.Show(
                    "Formatul Email-ului nu este valid.\n" +
                    "Exemplu corect: nume@domeniu.com",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        public static bool ValidareEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var trimmed = email.Trim();
            var atIndex = trimmed.IndexOf('@');

            if (atIndex <= 0 || atIndex == trimmed.Length - 1)
                return false;

            var domeniu = trimmed.Substring(atIndex + 1);
            if (!domeniu.Contains("."))
                return false;

            if (domeniu.StartsWith(".") || domeniu.EndsWith("."))
                return false;

            return true;
        }
    }
}