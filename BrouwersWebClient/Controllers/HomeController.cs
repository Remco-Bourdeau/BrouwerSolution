using BrouwersWebClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace BrouwersWebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory clientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            this.clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var model = new BrouwersViewModel();
            model.Brouwers = await GetBrouwers();
            if (model.Brouwers is not null)
                return View(model);
            else
                return RedirectToAction("Error", "Brouwers niet gevonden");
            /*
            var client = clientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:16436/brouwers");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var brouwers = await response.Content.ReadAsAsync<List<Brouwer>>();
                    return View(brouwers);
                case HttpStatusCode.NotFound:
                    return RedirectToAction("Error", "Brouwers niet gevonden");
                default:
                    return RedirectToAction("Error", "Technisch probleem, contacteer de helpdesk.");
            }
            */
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
        public async Task<List<Brouwer>> GetBrouwers()
        {
            var client = clientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:16436/brouwers");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await response.Content.ReadAsAsync<List<Brouwer>>();
                default:
                    return null;
            }
        }
        public async Task<Brouwer> GetBrouwer(int id)
        {
            var client = clientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:16436/brouwers/{id}");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await response.Content.ReadAsAsync<Brouwer>();
                default:
                    return null;
            }
        }
        public async Task<IActionResult> BrouwerDetail(int brouwerId)
        {
            var model = new BrouwersViewModel();
            model.Brouwers = await GetBrouwers();
            model.GeselecteerdeBrouwer = await GetBrouwer(brouwerId);
            return View("Index", model);
        }
    }
}
