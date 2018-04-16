﻿namespace Countrys.API.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Countrys.Domain;

    [Authorize]
    public class UbicationsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Ubications
        public async Task<ActionResult> Index()
        {
            return View(await db.Ubications.ToListAsync());
        }

        // GET: Ubications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubication ubication = await db.Ubications.FindAsync(id);
            if (ubication == null)
            {
                return HttpNotFound();
            }
            return View(ubication);
        }

        // GET: Ubications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ubications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UbicationId,Description,Address,Phone,Latitude,Longitude")] Ubication ubication)
        {
            if (ModelState.IsValid)
            {
                db.Ubications.Add(ubication);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ubication);
        }

        // GET: Ubications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubication ubication = await db.Ubications.FindAsync(id);
            if (ubication == null)
            {
                return HttpNotFound();
            }
            return View(ubication);
        }

        // POST: Ubications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UbicationId,Description,Address,Phone,Latitude,Longitude")] Ubication ubication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ubication).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ubication);
        }

        // GET: Ubications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubication ubication = await db.Ubications.FindAsync(id);
            if (ubication == null)
            {
                return HttpNotFound();
            }
            return View(ubication);
        }

        // POST: Ubications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ubication ubication = await db.Ubications.FindAsync(id);
            db.Ubications.Remove(ubication);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
