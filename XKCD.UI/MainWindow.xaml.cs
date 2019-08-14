using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using XKCD.Core.Service;
using XKCD.Core.ViewModel;

namespace XKCD.UI
{
    public partial class MainWindow : Window
    {
        private ComicViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            // set the current culture for the datetime converter
            this.Language = XmlLanguage.GetLanguage( CultureInfo.CurrentCulture.IetfLanguageTag );

            var comicService = new WebComicService();
            viewModel = new ComicViewModel(comicService);
            this.DataContext = viewModel;
        }

        private async void Window_Loaded( object sender, RoutedEventArgs e )
        {
            await viewModel.OnFirstShown();
        }
    }
}
