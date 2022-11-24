﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexitHisCore;
using FlexitHisMVC.Areas.Admin.Model;
using FlexitHisMVC.Data;
using FlexitHisMVC.Models;
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
                PersonalPageDTO response = new PersonalPageDTO();
                PersonalRepo personalRepo = new PersonalRepo(ConnectionString);
                SpecialityRepo  specialityRepo = new SpecialityRepo(ConnectionString);
                HospitalRepo  hospitalRepo = new HospitalRepo(ConnectionString);
              
                response.personalList = personalRepo.GetPersonalList();
                response.specialityList = specialityRepo.GetSpecialities();
                response.hospitalList = hospitalRepo.GetHospitalList();
              
                return View(response);


            }
            else
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }


           
        }
        [HttpGet]
        public IActionResult HospitalsByUser(int personalID)
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                HospitalRepo hospitalRepo = new HospitalRepo(ConnectionString);
                
                return Ok(hospitalRepo.GetHospitalListByUser(personalID));

            }
            else
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }



        }
        [HttpGet]
        public IActionResult DepartmentsByHospital(int hospitalID)
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                DepartmentsRepo departmentsRepo = new DepartmentsRepo(ConnectionString);

                return Ok(departmentsRepo.GetDepartmentsByHospital(hospitalID));

            }
            else
            {
                return Unauthorized();
            }



        }
        [HttpPost]
        public IActionResult Add(string name, string surname, string father,int specialityID, string passportSerialNum, string fin, string phone, string email, string bDate, string username, string pwd, int isUser,int isDr)
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                PersonalRepo personal = new PersonalRepo(ConnectionString);
               
                return Ok(personal.InsertPersonal( name,  surname,  father,specialityID,  passportSerialNum,  fin,  phone,  email,  bDate,  username,  pwd,  isUser, isDr));

            }
            else
            {
                return Unauthorized();
            }



        }
        [HttpPost]
        public IActionResult AddHospitalToUser(int userID,int hospitalID)
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                HospitalRepo hospital = new HospitalRepo(ConnectionString);

                return Ok(hospital.InsertHospital(userID, hospitalID));

            }
            else
            {
                return Unauthorized();
            }



        }
        [HttpPost]
        public IActionResult DeleteHospitalFromUser(int userID, int hospitalID)
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                HospitalRepo hospital = new HospitalRepo(ConnectionString);

                return Ok(hospital.InsertHospital(userID, hospitalID));

            }
            else
            {
                return Unauthorized();
            }



        }
        [HttpPost]
        public IActionResult Update()
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

