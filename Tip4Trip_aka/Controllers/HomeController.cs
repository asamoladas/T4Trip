using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Tip4Trip_aka.Models;
using Tip4Trip_aka.ViewModels;

namespace Tip4Trip_aka.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searching, DateTime? Sstartdate, DateTime? Enddate, int? occupantsearch)
        {
            var Hous = db.Houses.Include(xxx => xxx.Reservations).Include(mmn => mmn.Location).Where(x => x.Location.NameCity.Contains(searching) && x.MaxOccupancy >= occupantsearch);

            var res = db.Reservations.Where(c => (c.StartDate <= Sstartdate.Value) && (c.EndDate >= Sstartdate.Value) && (c.StartDate >= Sstartdate.Value) && (c.StartDate <= Enddate.Value) && (c.EndDate >= Enddate.Value) && (c.StartDate <= Enddate.Value));
            // var res2 = db.Reservations.Where(f => (f.EndDate >= Enddate.Value)&& (f.StartDate<= Enddate.Value));
            // var res3 = res.Concat(res2);
            if (Sstartdate == null && Enddate == null) { res = db.Reservations; }


            HousesDates to_search_mas = new HousesDates { houses = Hous.ToList(), reservations = res.ToList() };
            if (to_search_mas == null) { return View(); }
            return View(to_search_mas);

            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Search(string searching, DateTime? Sstartdate, DateTime? Enddate, int? occupantsearch)
        {
            string Searching = searching.ToUpper(); 
            var Hous = db.Houses.Include(xxx => xxx.Reservations).Include(mmn => mmn.Location).Where(x => x.Location.NameCity.Contains(Searching));// && x.Address.Contains(Address));

            var res = db.Reservations.Where(c => (c.StartDate <= Sstartdate.Value) && (c.EndDate >= Sstartdate.Value));
            var res2 = db.Reservations.Where(f => (f.EndDate >= Enddate.Value) && (f.StartDate <= Enddate.Value));
            var res4 = db.Reservations.Where(d => (d.StartDate >= Sstartdate.Value) && (d.StartDate <= Enddate.Value));
            var res3 = res.Concat(res2).Concat(res4);
            if (Sstartdate == null && Enddate == null) { res3 = db.Reservations; }

            //return View(db.Reservations.Include(mmn => mmn.HouseRes).Where(x => x.EndDate < StartDateSearch || x.StartDate > EndDateSearch || StartDateSearch == null || EndDateSearch == null).ToList());
            HousesDates to_search_mas = new HousesDates { houses = Hous.ToList(), reservations = res3.ToList() };
            if (to_search_mas == null) { ViewBag.Message = "T4TripSearCh."; return View(); }

            ViewBag.Message = "T4TripSearCh.";
            return View(to_search_mas);


        }
    }
}