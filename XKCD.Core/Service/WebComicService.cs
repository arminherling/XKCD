using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using XKCD.Core.Model;

namespace XKCD.Core.Service
{
    public class WebComicService : IComicService
    {
        private readonly HttpClient httpClient;

        public WebComicService( HttpClient httpClient )
        {
            this.httpClient = httpClient;
        }

        public async Task<Comic> LoadComic( int number = 0 )
        {
            string url = $"https://xkcd.com/{( number != 0 ? $"{number}/" : "" ) }info.0.json";

            using( var response = await httpClient.GetAsync( url ) )
            {
                if( response.IsSuccessStatusCode )
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var comicDTO = JsonConvert.DeserializeObject<ComicDTO>( responseJson );
                    return ComicFromDTO( comicDTO );
                }
                else
                {
                    return new Comic( -1, DateTime.Now, "An Error occured", response.ReasonPhrase, "", "" );
                }
            }
        }

        private Comic ComicFromDTO( ComicDTO comicDTO )
        {
            return new Comic(
                comicDTO.Num,
                new DateTime(
                    int.Parse( comicDTO.Year ),
                    int.Parse( comicDTO.Month ),
                    int.Parse( comicDTO.Day ) ),
                comicDTO.Title,
                comicDTO.Alt,
                comicDTO.Link,
                comicDTO.Img );
        }
    }
}
