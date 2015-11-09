using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buhta.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application 1 description page.";

            TestClass1.Test1();
            TestClass1.Test2();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }


        SchemaTable OrgTable;

        public ActionResult EditTable()
        {
            if (OrgTable == null)
            {
                OrgTable = new SchemaTable();
                OrgTable.ID = Guid.NewGuid();
                OrgTable.Name = @"Организ""ация";
                OrgTable.Description = "sprav org справочник орг";

            }
            var model = new SchemaTableEditModel();
            model.Table = OrgTable;
            return View(model);
        }

        private void X_OnChange(xInput sender, string newValue)
        {
            throw new NotImplementedException();
        }
    }
}