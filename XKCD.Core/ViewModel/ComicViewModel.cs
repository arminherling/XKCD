using System;
using System.Threading.Tasks;
using System.Windows.Input;
using XKCD.Core.Model;
using XKCD.Core.Service;

namespace XKCD.Core.ViewModel
{
    public class ComicViewModel : BaseViewModel
    {
        private readonly IComicService comicService;
        private int currentLastNumber;
        private Comic comic;
        private Comic Comic
        {
            set { SetProperty( ref this.comic, value ); }
            get { return this.comic; }
        }

        public string Title => Comic?.Title ?? string.Empty;
        public int Number => Comic?.Number ?? 0;
        public DateTime ReleaseDate => Comic?.ReleaseDate ?? new DateTime();
        public string Text => Comic?.Text ?? string.Empty;
        public string ImageSource => Comic?.ImageSource ?? string.Empty;
        public string Link => Comic?.Link ?? string.Empty;

        public ICommand FirstComicCommand { get; }
        public ICommand NextComicCommand { get; }
        public ICommand PreviousComicCommand { get; }
        public ICommand LastComicCommand { get; }
        public ICommand RandomComicCommand { get; }

        public ComicViewModel( IComicService comicService )
        {
            this.comicService = comicService;

            FirstComicCommand = new Command( OnFirstComic );
            NextComicCommand = new Command( OnNextComic );
            PreviousComicCommand = new Command( OnPreviousComic );
            LastComicCommand = new Command( OnLastComic );
            RandomComicCommand = new Command( OnRandomComic );
        }

        private void RefreshCanExecutes()
        {
            ( (Command)FirstComicCommand ).ChangeCanExecute();
            ( (Command)NextComicCommand ).ChangeCanExecute();
            ( (Command)PreviousComicCommand ).ChangeCanExecute();
            ( (Command)LastComicCommand ).ChangeCanExecute();
        }

        public async Task OnFirstShown()
        {
            comic = await comicService.LoadComic();
            currentLastNumber = comic.Number;
            RefreshCanExecutes();
        }

        private async void OnFirstComic( object parameter )
        {
            comic = await comicService.LoadComic( 1 );
            RefreshCanExecutes();
        }

        private async void OnNextComic( object parameter )
        {
            comic = await comicService.LoadComic( Number + 1 );
            RefreshCanExecutes();
        }

        private async void OnPreviousComic( object parameter )
        {
            comic = await comicService.LoadComic( Number - 1 );
            RefreshCanExecutes();
        }

        private async void OnLastComic( object parameter )
        {
            await OnFirstShown();
        }

        private async void OnRandomComic( object obj )
        {
            var random = new Random();
            var randomNumber = random.Next( 1, currentLastNumber + 1 );

            comic = await comicService.LoadComic( randomNumber );
            RefreshCanExecutes();
        }
    }
}
