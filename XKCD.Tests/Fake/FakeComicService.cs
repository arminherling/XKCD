using System.Collections.Generic;
using System.Threading.Tasks;
using XKCD.Core.Model;
using XKCD.Core.Service;

namespace XKCD.Tests.Fake
{
    class FakeComicService : IComicService
    {
        List<Comic> comics = new List<Comic>();

        public async Task<Comic> LoadComic( int number = 0 )
        {
            if( number == 0 )
            {
                var count = comics.Count;
                return comics[count - 1];
            }
            else
            {
                return comics.Find( x => x.Number == number );
            }
        }

        public void AddFake( Comic comic )
        {
            comics.Add( comic );
        }
    }
}
