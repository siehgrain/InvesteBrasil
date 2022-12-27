using InvesteBrasil.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace InvesteBrasil.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public string apiKey = "0WJIYVC4B5F8AJ4W";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public JsonResult MostraPrecos(string symbol)
        {
            try
            {
                string endpoint = "https://www.alphavantage.co/query";
                string function = "GLOBAL_QUOTE";
                string queryString = $"{endpoint}?function={function}&symbol={symbol}&apikey={apiKey}";
                Uri queryUri = new Uri(queryString);

                using (WebClient client = new WebClient())
                {
                    dynamic retorno = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(queryUri));
                    return Json(retorno);
                }    
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(ex); ;
            }
        }
        [HttpPost]
        public JsonResult Pesquisa(string keywords)
        {
            try
            {
                string endpoint = "https://www.alphavantage.co/query";
                string function = "SYMBOL_SEARCH";
                string queryString = $"{endpoint}?function={function}&keywords={keywords}&apikey={apiKey}";
                Uri queryUri = new Uri(queryString);

                using (WebClient client = new WebClient())
                {
                    dynamic retorno = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(queryUri));
                    return Json(retorno);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(ex); ;
            }
        }
    }
}