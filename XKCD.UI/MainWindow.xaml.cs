using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
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

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json") );

            var comicService = new WebComicService(httpClient);
            viewModel = new ComicViewModel(comicService);
            this.DataContext = viewModel;
        }

        private async void Window_Loaded( object sender, RoutedEventArgs e )
        {
            await viewModel.OnFirstShown();
        }
    }
}
