using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XKCD.Core.Model;
using XKCD.Core.Service;

namespace XKCD.Tests.Fake
{
    class FakeComicService : IComicService
    {
        List<Comic> comics = new List<Comic>();

        public Task<Comic> LoadComic()
        {
            var count = comics.Count;
            var comic = comics[count - 1];
            return Task.FromResult( comic );
        }

        public void AddFake( Comic comic )
        {
            comics.Add( comic );
        }
    }
}
