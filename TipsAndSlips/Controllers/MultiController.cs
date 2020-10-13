using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TipsAndSlips.Models;

namespace TipsAndSlips.Controllers
{
    public class MultiController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient _client = new HttpClient();
        private String _apiUrl;

        public MultiController(ILogger<HomeController> logger)
        {
#if DEBUG
            _apiUrl = "http://localhost:7071";
#else
            _apiUrl = "http://betsfriendsapi.azurewebsites.net";
#endif
            _logger = logger;
        }

        public IActionResult Index(string id)
        {            
            var content = _client.GetStringAsync(_apiUrl + "/api/GetMulti?count=1&id="+ id).Result;
            var multis = JsonConvert.DeserializeObject<List<MultiBuilder>>(content);
            var multi = multis.First();
            multi.FinalMarkets.Sort();
            ViewData["MultiData"] = multi;
            return View(multi);
        }

        public IActionResult List()
        {
            var content = _client.GetStringAsync(_apiUrl + "/api/GetMulti?count=1000").Result;
            var multis = JsonConvert.DeserializeObject<List<MultiBuilder>>(content);
            ViewData["ListMultiData"] = multis;
            return View(multis);
        }

        public IActionResult UpdateResults(string id)
        {
            _client.GetStringAsync(_apiUrl + "/api/UpdateMultiResults?id=" + id);
            return RedirectToAction("Index", new { id });
        }

        public IActionResult UpdateResultsAll()
        {
            _client.GetStringAsync(_apiUrl + "/api/UpdateMultiResults?id=");
            return RedirectToAction("List");
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
    }
}
