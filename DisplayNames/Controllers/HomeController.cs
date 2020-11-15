using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DisplayNames.Models;
using Microsoft.Extensions.Caching.Distributed;
using DisplayNames.Extensions;
using DisplayNames.FakeData;

namespace DisplayNames.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDistributedCache _cache;

        public HomeController(ILogger<HomeController> logger, IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Persons()
        {
            string recordkey = $"Persons_{ DateTime.Now.ToString("yyyyMMdd_hhmm") }";
            bool cachingLocation = false;
            IEnumerable<PersonModel> persons;

            try
            {
                persons = await _cache.GetRecordAsync<IEnumerable<PersonModel>>(recordkey);

                if (persons is null)
                {
                    persons = await DataPersons.Generate(100);

                    // setting caching data
                    await _cache.SetRecordAsync<IEnumerable<PersonModel>>(recordkey, persons);
                } 
                else
                {
                    cachingLocation = true;
                }
            }
            catch (System.Exception)
            {
                persons = await DataPersons.Generate(100);
            }
            

            ViewBag.Caching = cachingLocation;

            return View(persons);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
