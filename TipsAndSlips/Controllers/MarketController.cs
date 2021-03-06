﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TipsAndSlips.Models;

namespace TipsAndSlips.Controllers
{
    public class MarketController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient _client = new HttpClient();
        private String _apiUrl;

        public MarketController(ILogger<HomeController> logger)
        {
#if DEBUG
            _apiUrl = "http://localhost:7071";
#else
            _apiUrl = "http://betsfriendsapi.azurewebsites.net";
#endif
            _logger = logger;
        }

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
                
        public async Task<String> Display(String marketId)
        {
            var content = await _client.GetStringAsync(_apiUrl + "/api/BFSearch?searchText=" + marketId);
            return content;
        }
    }
}
