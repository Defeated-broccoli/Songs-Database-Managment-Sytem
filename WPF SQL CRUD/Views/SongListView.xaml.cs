using System.Windows.Controls;
using WPF_SQL_CRUD.ViewModels;

namespace WPF_SQL_CRUD.Views
{
    /// <summary>
    /// Interaction logic for SongListView.xaml
    /// </summary>
    public partial class SongListView : Page
    {
        public SongListView()
        {
            InitializeComponent();

            DataContext = new SongListViewModel();
        }
    }
}
