using System;
using System.Threading.Tasks;
using XKCD.Core.Model;

namespace XKCD.Core.Service
{
    public class WebComicService : IComicService
    {
        public async Task<Comic> LoadComic( int i = 0 )
        {
            return new Comic( 1, DateTime.Now, "title", "text", "link", "" );
        }
    }
}
