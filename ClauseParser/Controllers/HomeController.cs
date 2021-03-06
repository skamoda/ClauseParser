﻿using ClauseParser.Code.Services;
using ClauseParser.Code.Services.Parser;
using ClauseParser.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ClauseParser.Models.Symbol;

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
                steps = GetExceptionResult(ex);
            }
            
            var json = JsonConvert.SerializeObject(steps, new SymbolsJsonConverter());
            return Json(json);
        }

        private static List<Step> GetExceptionResult(Exception ex)
        {
            var constant = new Constant(null, "Incorrect data provided");

            return new List<Step>
            {
                new Step(new List<Symbol> {constant})
                {
                    Top = constant,
                    Title = constant.Name
                }
            };
        }
    }
}
