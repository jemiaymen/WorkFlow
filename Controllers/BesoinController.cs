using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkFlow.Models;

namespace WorkFlow.Controllers
{
    public class BesoinController : WorkController
    {

        // GET: Besoin
        public async Task<ActionResult> Index()
        {
            ApplicationUser current = db.Users.Where(u => u.UserName == User.Identity.Name).First();
            var achats = db.Achats.Include(a => a.Department).Where(a => a.Type == Models.Type.Besoin && a.DepartmentID == current.DepartmentID);
            return View(await achats.ToListAsync());
        }

        // GET: Besoin
        public async Task<ActionResult> IndexDem()
        {
            var achats = db.Achats.Include(a => a.Department).Where(a => a.Type == Models.Type.Besoin);
            return View(await achats.ToListAsync());
        }


        // GET: Achat/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Dep");
            ViewBag.Categ = new SelectList(db.Categories, "Lbl", "Lbl");
            ApplicationUser current = db.Users.Where(u => u.UserName == User.Identity.Name).First();

            ViewBag.depd = current.Department.Budget;
            ViewBag.depdt = current.Department.Depense;
            return View();
        }

        // POST: Achat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Des,Categ,DtAcha,Creation,LieuLiv,Imp,Qte,Type")] Achat achat)
        {
            ApplicationUser current = db.Users.Where(u => u.UserName == User.Identity.Name).First();
            achat.DepartmentID = current.DepartmentID;

            if (ModelState.IsValid)
            {
                db.Achats.Add(achat);
                Notification notif = new Notification{ Achat = achat, Lbl = "Besoin Créer Par " + current.Login + " de Department " + current.Department.Dep , Type = NType.BesoinCreer };
                db.Notifications.Add(notif);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }



            ViewBag.Categ = new SelectList(db.Categories, "Lbl", "Lbl", achat.Categ);

            ViewBag.depd = current.Department.Budget;
            ViewBag.depdt = current.Department.Depense;

            return View(achat);
        }

        // GET: Besoin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achat achat = await db.Achats.FindAsync(id);
            if (achat == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Dep", achat.DepartmentID);
            return View(achat);
        }

        // POST: Besoin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,DepartmentID,Des,Categ,DtAcha,Creation,LieuLiv,Imp,Qte")] Achat achat)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser current = db.Users.Where(u => u.UserName == User.Identity.Name).First();

                db.Entry(achat).State = EntityState.Modified;
                Notification notif = new Notification { Achat = achat, Lbl = "Besoin Modifier Par " + current.Login + " de Department " + current.Department.Dep, Type = NType.BesoinModifierParDemandeur };
                db.Notifications.Add(notif);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Dep", achat.DepartmentID);
            return View(achat);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
