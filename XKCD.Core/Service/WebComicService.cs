using System;
using System.Threading.Tasks;
using XKCD.Core.Model;

namespace XKCD.Core.Service
{
    class WebComicService : IComicService
    {
        public Task<Comic> LoadComic( int i = 0 )
        {
            throw new NotImplementedException();
        }
    }
}
