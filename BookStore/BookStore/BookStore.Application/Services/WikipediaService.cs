using BookStore.Data.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BookStore.Application.Services
{
    public class WikipediaService : IExternalSearchService
    {
        private const string WikipediaSearchUrl = "https://en.wikipedia.org/w/api.php?action=query&generator=search&format=json&gsrprop=snippet&prop=info&inprop=url&gsrsearch={0}";

        public List<SearchResult> Search(string text)
        {
            var client = new WebClient();
            var json = client.DownloadString(string.Format(WikipediaSearchUrl, HttpUtility.UrlEncode(text)));
            var result = JObject.Parse(json);
            var pages = result["query"]["pages"];

            var searchResults = new List<SearchResult>();

            foreach (JProperty page in pages)
            {
                var searchResult = new SearchResult
                {
                    Title = page.Value["title"].ToString(),
                    Url = page.Value["fullurl"].ToString()
                };
                searchResults.Add(searchResult);
            }

            return searchResults;
        }
    }
}
