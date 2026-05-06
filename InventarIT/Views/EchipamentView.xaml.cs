using System.Windows.Controls;
using InventarIT.Data;

namespace InventarIT.Views
{
    public partial class EchipamentView : Page
    {
        private readonly DatabaseHelper _db = new();

        public EchipamentView()
        {
            InitializeComponent();
            IncarcaDate();
        }

        private void IncarcaDate()
        {
            dgEchipamente.ItemsSource = _db.GetAllEchipamente();
        }
    }
}