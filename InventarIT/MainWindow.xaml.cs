using System.Windows;
using System.Windows.Navigation;
using InventarIT.Data;
using InventarIT.Views;

namespace InventarIT
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            VerificaConexiunea();
        }

        private void VerificaConexiunea()
        {
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

        private void NavigateEchipamente(object sender, RoutedEventArgs e)
        {
            txtModulActiv.Text = "— Echipamente";
            MainFrame.Navigate(new EchipamentView());
        }

        private void NavigateAngajati(object sender, RoutedEventArgs e)
        {
            txtModulActiv.Text = "— Angajați";
            MainFrame.Navigate(new AngajatView());
        }

        private void NavigateAtribuiri(object sender, RoutedEventArgs e)
        {
            txtModulActiv.Text = "— Atribuiri";
            MainFrame.Navigate(new AtribuireView());
        }

        private void NavigateRaport(object sender, RoutedEventArgs e)
        {
            txtModulActiv.Text = "— Raport";
            var raport = new RaportView();
            raport.ShowDialog();
        }
    }
}