using System;
using ClauseParser.Code.Services.Parser;
using ClauseParser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ClauseParser.Code.Services;
using Newtonsoft.Json;

namespace ClauseParser.Controllers
{
    /// <summary>
    /// Main controller
    /// </summary>
    public class HomeController : Controller
    {
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
            ParserService parserService = new ParserService();
            List<Step> steps = null;

            try
            {
                steps = parserService.Parse(text);
            }
            catch (Exception ex)
            {
                //return error ie bad syntax
            }

            var json = JsonConvert.SerializeObject(steps, new SymbolsJsonConverter());
            return Json(json);
        }
    }
}
