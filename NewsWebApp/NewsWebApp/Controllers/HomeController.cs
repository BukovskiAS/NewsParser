using Microsoft.AspNetCore.Mvc;
using NewsLibrary;
using NewsLibrary.Models;
using NewsWebApp.ViewModels;
using NewsWebApp.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebApp.Controllers
{
    public class HomeController : Controller
	{
		private readonly Repository _repository = new();  

		public HomeController() { }
		
		public async Task<IActionResult> Index() => View(await _repository.GetNewsAsync());

		public IActionResult News(NewsSetting setting) => PartialView(nameof(News), GenerateSelection(setting));

		public IEnumerable<NewsContain> GenerateSelection(NewsSetting setting) =>		
			_repository.GetNews().
			Where(x => x.Source == setting.Source || setting.Source == "all").
			AsQueryable().OrderByProperty(setting.Sort).ToList(); 		  
	}
}