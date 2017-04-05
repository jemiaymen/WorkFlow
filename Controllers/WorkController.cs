using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using WorkFlow.Models;


namespace WorkFlow.Controllers
{
    public class WorkController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var not = db.Notifications.Include(n => n.Achat).Where(n => n.Etat == false && (n.Achat.Type == Models.Type.Besoin || (n.Achat.Type == Models.Type.Demande && n.Type == NType.DemandeModification))).Count();
            var dem = db.Notifications.Where(n => n.Etat == false && (n.Type == NType.BesoinRefuser || n.Type == NType.DemandeCreer || n.Type == NType.DemandeModifierParRespAchat)).Count();
            ViewBag.RespNotif = not + "";
            ViewBag.DemNotif = dem + "";
            base.OnActionExecuting(filterContext);
        }
    }
}