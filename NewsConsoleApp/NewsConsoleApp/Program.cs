using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using NewsLibrary;
using NewsLibrary.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using System.IO;
using Newtonsoft.Json;

namespace NewsConsoleApp
{
	class Program
	{
		#region Fields 
		private static readonly Dictionary<string,string> urlList = new() {
			{ "habr", "https://habr.com/ru/rss/all/all" }, 
			{ "ixbt", "http://www.ixbt.com/export/news.rss" }}; 
		#endregion

		static void Main(string[] args)
		{ 
			foreach (var url in urlList)
			{
				GetHtpp(url).Wait(); 
			}  
		}

		static async Task GetHtpp(KeyValuePair<string, string> url)
		{
			try
			{ 
				var response = await new HttpClient().GetAsync(url.Value);
				var responseBody = await response.Content.ReadAsStringAsync();
				var document = new HtmlParser().ParseDocument(responseBody);
				SaveActualNews(document, url.Key); 
			}
			catch 
			{
                //ignore
			}
		}

		static Repository GetRepository()
		{
			using var sr = new StreamReader("./env/env.json");
			var config = JsonConvert.DeserializeObject<ConfigModel>(sr.ReadToEnd());
			return new Repository(config.ConnectionString);
		}

		static void SaveActualNews(IHtmlDocument document, string source)
		{ 
			var repository = GetRepository();
			var newsList = new List<NewsContain>();

			foreach (IElement element in document.QuerySelectorAll("item"))
            { 
				newsList.Add(new NewsContain
                {
                    Source = source,
                    Link = element.QuerySelector("guid").Text(),
                    Title = element.QuerySelector("title").Text(),
                    Description = element.QuerySelector("description").Text(),
                    PubDate = Convert.ToDateTime(element.QuerySelector("pubdate").Text())
                });
            }

            var newNews = repository.GetUniqueNews(newsList);

            try
			{
				repository.SetNews(newNews);
				Console.WriteLine($"Processed {newsList.Count} news, added of thease {newNews.Count()} from {source}");
			}
            catch (Exception e)
            {
                Console.WriteLine($"{source} \t {e.InnerException}");
            }
		} 
	}
}
