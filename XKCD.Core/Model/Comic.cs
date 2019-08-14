using System;

namespace XKCD.Core.Model
{
    public class Comic
    {
        public int Number { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string ImageSource { get; private set; }
        public string Link { get; private set; }

        public Comic( int number, DateTime releaseDate, string title, string text, string link, string imageSource )
        {
            Number = number;
            ReleaseDate = releaseDate;
            Title = title;
            Text = text;
            Link = link;
            ImageSource = imageSource;
        }
    }
}
