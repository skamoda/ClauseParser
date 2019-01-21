using ClauseParser.Code.Services;
using ClauseParser.Code.Services.Parser;
using ClauseParser.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
            
            var json = JsonConvert.SerializeObject(steps, new SymbolsJsonConverter());
            return Json(json);
        }
    }
}
