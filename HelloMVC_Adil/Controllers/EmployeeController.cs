using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelloMVC_Adil.Models;

namespace HelloMVC_Adil.Controllers
{
    public class EmployeeController : Controller
    {
        // "Server=MYSQL5030.site4now.net;Database=db_a71f72_niitest;Uid=a71f72_niitest;Pwd=qwerty123"

        /*
         *  id  name_                   title   salary
            101	Yespembetova Ayazhan	201	    750
            102	Tulepbergen Sagynysh	202	    680
            103	Bilatov Arman	        203	    570
            104	Myktybek Adil	        204	    750
         * 
         */

        HelloMVC_Adil.DB.DB db { get; set; }
        public EmployeeController()
        {
            db = new HelloMVC_Adil.DB.DB();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<dbTest> bookList = db.getAllBooks();
            return View(bookList);
        }

        [HttpGet]
        public ActionResult create()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult create(dbTest mb)
        {
            db.add(mb);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Edit(int id)  // int id
        {
            dbTest dbTests = db.getOnlyOne(id);
            return View(dbTests);  // idModel
        }

        /*public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<dbTest> idModel = await dbTest.FindAsync(id);
            if (idModel == null)
            {
                return HttpNotFound();
            }
            return View(idModel);
        }*/

        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(dbTest mb)
        {
            db.edit(mb);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult delete(int id)
        {
            dbTest dbTests = db.getOnlyOne(id);
            return View(dbTests);
        }

        [HttpPost, ActionName("delete")]
        public ActionResult deleteConfirmed(dbTest mb)
        {
            db.delete(mb);
            return RedirectToAction("index");
        }


        [HttpGet]
        public ActionResult details(int id)
        {
            dbTest dbTests = db.getOnlyOne(id);
            if (dbTests == null)
            {
                return HttpNotFound();
            }
            return View(dbTests);
        }
    }
}