using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using InventarIT.Models;

namespace InventarIT.Helpers
{
    public static class ExportHelper
    {
        public static void ExportRaportTxt(
            List<RaportAngajat> date,
            StatisticiInventar stats)
        {
            var dialog = new SaveFileDialog
            {
                Title = "Salvează Raport",
                FileName = $"Raport_InventarIT_" +
                             $"{DateTime.Today:yyyyMMdd}.txt",
                DefaultExt = ".txt",
                Filter = "Text files (*.txt)|*.txt"
            };

            if (dialog.ShowDialog() != true) return;

            try
            {
                using var writer =
                    new StreamWriter(dialog.FileName);
                writer.WriteLine(
                    "================================================");
                writer.WriteLine(
                    "       RAPORT SUMAR INVENTAR IT");
                writer.WriteLine(
                    $"       Data: {DateTime.Now:dd.MM.yyyy HH:mm}");
                writer.WriteLine(
                    "================================================");
                writer.WriteLine();

                writer.WriteLine("STATISTICI GENERALE:");
                writer.WriteLine(
                    $"  Total angajați cu echipamente: " +
                    $"{stats.TotalAngajati}");
                writer.WriteLine(
                    $"  Valoare totală atribuită:      " +
                    $"{stats.ValoareTotala:N2} lei");
                writer.WriteLine(
                    $"  Medie valoare per angajat:     " +
                    $"{stats.MediePerAngajat:N2} lei");
                writer.WriteLine(
                    $"  Cel mai valoros echipament:    " +
                    $"{stats.CelMaiValoros}");
                writer.WriteLine();

                writer.WriteLine(
                    "------------------------------------------------");
                writer.WriteLine(
                    $"{"#",-4}" +
                    $"{"Angajat",-25}" +
                    $"{"Departament",-15}" +
                    $"{"Nr.Ech.",-10}" +
                    $"{"Valoare (lei)",15}");
                writer.WriteLine(
                    "------------------------------------------------");

                foreach (var r in date)
                {
                    writer.WriteLine(
                        $"{r.Rand,-4}" +
                        $"{r.NumeAngajat,-25}" +
                        $"{r.Departament,-15}" +
                        $"{r.NrEchipamente,-10}" +
                        $"{r.ValoareTotala,15:N2}");
                }

                writer.WriteLine(
                    "================================================");
                writer.WriteLine(
                    "  Raport generat de InventarIT Application");
                writer.WriteLine(
                    "================================================");

                MessageBox.Show(
                    $"Raportul a fost exportat cu succes!\n" +
                    $"{dialog.FileName}",
                    "Export reușit",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Eroare la export:\n{ex.Message}",
                    "Eroare", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}