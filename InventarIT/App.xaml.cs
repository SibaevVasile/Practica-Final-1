using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using InventarIT.Data;

namespace InventarIT
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var db = new DatabaseHelper();
            if (!db.TesteazaConexiunea())
            {
                MessageBox.Show(
                    "Nu s-a putut conecta la baza de date!\n" +
                    "Verificați că SQL Server Express rulează.",
                    "Eroare conexiune",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}