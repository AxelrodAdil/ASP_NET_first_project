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
    public class WebPageController : Controller
    {
        // select * from db_a71f72_niitest.logindata
        // "Server=MYSQL5030.site4now.net;Database=db_a71f72_niitest;Uid=a71f72_niitest;Pwd=qwerty123"

        /*
         *  titleId     tName                   locationId
            201	        web developer	        5879
            202	        data analyst	        1265
            203	        software developer	    2987
            204	        fullstack developer	    7526
         * 
         */

        HelloMVC_Adil.Data.WebPageDB db { get; set; }
        public WebPageController()
        {
            db = new HelloMVC_Adil.Data.WebPageDB();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<dbWeb> bookList = db.getAllBooks();
            return View(bookList);
        }

        [HttpGet]
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(dbWeb mb)
        {
            db.add(mb);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Edit(int id)  // int id
        {
            dbWeb dbWebs = db.getOnlyOne(id);
            return View(dbWebs);  // idModel
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(dbWeb mb)
        {
            db.edit(mb);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult delete(int id)
        {
            dbWeb dbWebs = db.getOnlyOne(id);
            return View(dbWebs);
        }

        [HttpPost, ActionName("delete")]
        public ActionResult deleteConfirmed(dbWeb mb)
        {
            db.delete(mb);
            return RedirectToAction("index");
        }


        [HttpGet]
        public ActionResult details(int id)
        {
            dbWeb dbWebs = db.getOnlyOne(id);
            if (dbWebs == null)
            {
                return HttpNotFound();
            }
            return View(dbWebs);
        }
    }
}