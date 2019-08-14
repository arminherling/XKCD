﻿using System.Threading.Tasks;
using XKCD.Core.Model;

namespace XKCD.Core.Service
{
    public interface IComicService
    {
        Task<Comic> LoadComic(int i = 0);
    }
}
