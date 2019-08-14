using System.Threading.Tasks;
using XKCD.Core.Model;
using XKCD.Core.Service;

namespace XKCD.Core.ViewModel
{
    public class ComicViewModel
    {
        private readonly IComicService comicService;
        private Comic comic;

        public string Title => comic?.Title ?? "";
        public int Number => comic?.Number ?? 0;

        public ComicViewModel( IComicService comicService )
        {
            this.comicService = comicService;
        }

        public async Task OnFirstShown()
        {
            comic = await comicService.LoadComic();
        }
    }
}
