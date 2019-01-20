using System;
using ClauseParser.Code.Services.Parser;
using ClauseParser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ClauseParser.Code.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ClauseParser.Controllers
{
    /// <summary>
    /// Main controller
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IParserService _parserService;
        private readonly ILogger _logger;

        public HomeController(
            IParserService parserService,
            ILogger<HomeController> logger
        )
        {
            _parserService = parserService;
            _logger = logger;
        }
        
        /// <summary>
        /// Main page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Action responsible for parsing clause
        /// </summary>
        /// <param name="text">Clause text</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Parse(string text)
        {
            List<Step> steps = null;

            try
            {
                steps = _parserService.Parse(text);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                //return error ie bad syntax
            }

            var json = JsonConvert.SerializeObject(steps, new SymbolsJsonConverter());
            return Json(json);
        }
    }
}
