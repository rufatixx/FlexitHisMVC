﻿using System;
using FlexitHisCore;
using FlexitHisMVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace FlexitHisMVC.Areas.Admin.ViewComponents
{
    public class PersonalSettingsViewComponent : ViewComponent
    {
        private readonly string ConnectionString;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public IConfiguration Configuration;
        //Communications communications;
        public PersonalSettingsViewComponent(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionString").Value;
            _hostingEnvironment = hostingEnvironment;
            //communications = new Communications(Configuration, _hostingEnvironment);
        }
        public IViewComponentResult Invoke()
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                PersonalRepo personal = new PersonalRepo(ConnectionString);

                return View(personal.GetPersonalList());

            }
            else
            {
                return View();
            }


        }
        //[HttpPost]
        //public IActionResult UpdatePersonal(int userID, )
        //{
        //    if (HttpContext.Session.GetInt32("userid") != null)
        //    {
        //        PersonalOperations personal = new PersonalOperations(ConnectionString);

        //        return new OkObjectResult(new { });

        //    }
        //    else
        //    {
        //        return new UnauthorizedObjectResult(new { });
        //    }


        //}
    }
}
