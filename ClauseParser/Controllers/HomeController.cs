using System;
using ClauseParser.Code.Services.Parser;
using ClauseParser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ClauseParser.Code.Services;
using Newtonsoft.Json;

namespace ClauseParser.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

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

            var a = Newtonsoft.Json.JsonConvert.SerializeObject(steps[0].Top, new SymbolsJsonConverter());  //();
            return Json(steps);
        }
    }
}
