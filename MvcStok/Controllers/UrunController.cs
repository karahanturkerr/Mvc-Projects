using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {

        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();

            return View(degerler);
        }

        [HttpGet]
        public ActionResult UrunEkle() { 
            
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View(); 
  
        }

        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1) {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORID == p1.TBLKATEGORILER.KATEGORID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }
        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}