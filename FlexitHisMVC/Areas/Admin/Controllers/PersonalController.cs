﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexitHisCore;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlexitHisMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonalController : Controller
    {
        private readonly string ConnectionString;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public IConfiguration Configuration;
        //Communications communications;
        public PersonalController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionString").Value;
            _hostingEnvironment = hostingEnvironment;
            //communications = new Communications(Configuration, _hostingEnvironment);
        }
     
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }


           
        }
        
    }
}

