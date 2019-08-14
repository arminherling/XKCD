using System;
using System.Threading.Tasks;
using System.Windows.Input;
using XKCD.Core.Model;
using XKCD.Core.Service;

namespace XKCD.Core.ViewModel
{
    public class ComicViewModel
    {
        private readonly IComicService comicService;
        private Comic comic;
        private int currentLastNumber;

        public string Title => comic?.Title ?? string.Empty;
        public int Number => comic?.Number ?? 0;
        public DateTime ReleaseDate => comic?.ReleaseDate ?? new DateTime();
        public string Text => comic?.Text ?? string.Empty;
        public string ImageSource => comic?.ImageSource ?? string.Empty;
        public string Link => comic?.Link ?? string.Empty;

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

        public async Task OnFirstShown()
        {
            comic = await comicService.LoadComic();
            currentLastNumber = comic.Number;
        }

        private async void OnFirstComic( object parameter )
        {
            comic = await comicService.LoadComic( 1 );
        }

        private async void OnNextComic( object parameter )
        {
            comic = await comicService.LoadComic( Number + 1 );
        }

        private async void OnPreviousComic( object parameter )
        {
            comic = await comicService.LoadComic( Number - 1 );
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
        }
    }
}
