﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KPMG_Assessment.Website.Models;
using Microsoft.AspNetCore.Http;
using KPMG_Assessment.Website.Services;

namespace KPMG_Assessment.Website.Controllers
{

    public class HomeController : Controller
    {

        public HomeController()
        {
                 
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
