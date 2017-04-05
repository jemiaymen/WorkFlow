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
    public class DemandeController : WorkController
    {


        // GET: RespNotif
        public async Task<ActionResult> RespNotif()
        {
            var notifications = db.Notifications.Include(n => n.Achat).Where(n => n.Etat == false && (n.Achat.Type == Models.Type.Besoin || ( n.Achat.Type == Models.Type.Demande && n.Type == NType.DemandeModification ) ));
            return View(await notifications.ToListAsync());
        }


        // GET: DemNotif
        public async Task<ActionResult> DemNotif()
        {
            var notifications = db.Notifications.Include(n => n.Achat).Where(n => n.Etat == false && (n.Type == NType.BesoinRefuser || n.Type == NType.DemandeCreer || n.Type == NType.DemandeModifierParRespAchat ) );
            return View(await notifications.ToListAsync());
        }


        public async Task<ActionResult> Accept(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = await db.Notifications.FindAsync(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            ApplicationUser current = db.Users.Where(u => u.UserName == User.Identity.Name).First();

            notification.Etat = true;
            notification.Achat.Type = Models.Type.Demande;
            db.Entry(notification).State = EntityState.Modified;
            Notification notif = new Notification { Achat = notification.Achat, Lbl = "Besoin Accepter Par " + current.Login , Type = NType.DemandeCreer };
            db.Notifications.Add(notif);

            await db.SaveChangesAsync();
            return RedirectToAction("RespNotif");

        }

        public async Task<ActionResult> Reject(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = await db.Notifications.FindAsync(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            ApplicationUser current = db.Users.Where(u => u.UserName == User.Identity.Name).First();

            notification.Etat = true;
            db.Entry(notification).State = EntityState.Modified;
            Notification notif = new Notification { Achat = notification.Achat, Lbl = "Besoin Refuser Par " + current.Login, Type = NType.DemandeCreer };
            db.Notifications.Add(notif);

            await db.SaveChangesAsync();
            return RedirectToAction("RespNotif");

        }

        public async Task<ActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = await db.Notifications.FindAsync(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        public async Task<ActionResult> Vue(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = await db.Notifications.FindAsync(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
           
            notification.Etat = true;
            notification.Achat.Type = Models.Type.Demande;
            db.Entry(notification).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return RedirectToAction("DemNotif");

        }

        public async Task<ActionResult> Dem()
        {
            ApplicationUser current = db.Users.Where(u => u.UserName == User.Identity.Name).First();
            var achats = db.Achats.Include(a => a.Department).Where(a => a.Type == Models.Type.Demande && a.DepartmentID == current.DepartmentID);
            return View(await achats.ToListAsync());
        }
       
        public async Task<ActionResult> DemEdit(int? id)
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

            ApplicationUser current = db.Users.Where(u => u.UserName == User.Identity.Name).First();

            DemandeModification dm = new DemandeModification { ID = id, Login = current.Login , Des = achat.Des};
            return View(dm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DemEdit([Bind(Include = "ID,Login,Des,Msg")] DemandeModification dem)
        {
            if (ModelState.IsValid)
            {
                Notification notif = new Notification { AchatID = dem.ID.GetValueOrDefault() , Lbl = dem.Msg + " , Par :" + dem.Login, Type = NType.DemandeModification };
                db.Notifications.Add(notif);
                await db.SaveChangesAsync();

                return RedirectToAction("Dem");
            }

            return View(dem);

        }


        // GET: Besoin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notif = await db.Notifications.FindAsync(id);
            if (notif == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Dep", notif.Achat.DepartmentID);
            return View(notif);
        }

        // POST: Besoin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,DepartmentID,Des,Categ,DtAcha,Creation,LieuLiv,Imp,Qte")] Achat achat, int id, int DepartmentID)
        {
            if (ModelState.IsValid)
            {
                achat.DepartmentID = DepartmentID;
                achat.Type = Models.Type.Demande;

                ApplicationUser current = db.Users.Where(u => u.UserName == User.Identity.Name).First();
                Notification n = await db.Notifications.FindAsync(id);
                n.Etat = true;
                db.Entry(n).State = EntityState.Modified;
                db.Entry(achat).State = EntityState.Modified;
                Notification notif = new Notification { Achat = achat, Lbl = "Demande Modifier Par : " + current.Login, Type = NType.DemandeModifierParRespAchat };
                db.Notifications.Add(notif);
                await db.SaveChangesAsync();
                return RedirectToAction("RespNotif");
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
