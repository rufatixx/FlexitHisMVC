using System;
using System.Collections.Specialized;
using FlexitHisMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlexitHisMVC.ViewComponents
{

    public class ModulesViewComponent : ViewComponent
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public IConfiguration Configuration;
        //Communications communications;
        public ModulesViewComponent(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;

            _hostingEnvironment = hostingEnvironment;
            //communications = new Communications(Configuration, _hostingEnvironment);
        }
        public IViewComponentResult Invoke(int id)
        {
            var submoduleList = new List<Module>();
            Module module = new Module();
            switch (id)

            {
                case 1:
                    module.name = "Şöbə";
                    module.url = "/Admin/Department";
                    module.img = "/res/admin/department.png";
                    submoduleList.Add(module);

                    module.name = "Təşkilar";
                    module.img = "/res/admin/company.png";
                    module.url = "/Admin/Company";
                    submoduleList.Add(module);

                    module.name = "Personal";
                    module.url = "/Admin/Personal";
                    module.img = "/res/admin/personal.png";
                    submoduleList.Add(module);

                    //module.name = "Səbəb tərifi";
                    //module.url = "/Admin/Personal";
                    //submoduleList.Add(module);

                    //module.name = "Diaqnoz tərifi";
                    //module.url = "/Admin";
                    //submoduleList.Add(module);

                    //module.name = "Anbar";
                    //module.url = "/Admin";
                    //submoduleList.Add(module);

                    //module.name = "Sıra təqib";
                    //module.url = "/Admin";
                    //submoduleList.Add(module);

                    //module.name = "Sistem log məlumatları";
                    //module.url = "/Admin";
                    //submoduleList.Add(module);

                    //module.name = "Tərcümələr";
                    //module.url = "/Admin";
                    //submoduleList.Add(module);

                    //module.name = "Parametrlər";
                    //module.url = "/Admin";
                    //submoduleList.Add(module);

              
                    break;
                case 2:
                    break;
                default:
                    module.name = "Xəstə qəbulu";
                    module.id = "newPatientButton";
                    module.img = "/res/new_patient.png";
                    module.url = "/NewPatient";
                    submoduleList.Add(module);

                    module.name = "Kassa";
                    module.id = "kassaButton";
                    module.img = "/res/kassa/kassa.png";
                    module.url = "/";
                    submoduleList.Add(module);

                    module.name = "Poliklinika";
                    module.id = "policlinicButton";
                    module.img = "/res/policlinic.png";
                    module.url = "/";
                    submoduleList.Add(module);

                    module.name = "Admin";
                    module.id = "adminButton";
                    module.img = "/res/admin/admin.png";

                    module.url = "/Admin";
                    submoduleList.Add(module);
                    break;

            }

            //dynamic submodule = new System.Dynamic.ExpandoObject();
            return View(submoduleList);


        }

    }

}

