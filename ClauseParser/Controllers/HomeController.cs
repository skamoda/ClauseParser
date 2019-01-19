using System;
using ClauseParser.Code.Services.Parser;
using ClauseParser.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Parse(string text)
        {
            ParserService parserService = new ParserService();

            try
            {
                List<Step> steps = parserService.Parse(text);
            }
            catch (Exception ex)
            {
                //return error ie bad syntax
            }

            //ViewBag.stepList = steps;

            return View("Index");
        }
    }
}
