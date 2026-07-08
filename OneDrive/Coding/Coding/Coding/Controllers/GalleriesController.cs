using Coding.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Coding.Controllers
{
    public class GalleriesController : Controller
    {
        private CollectionDbContext db = new CollectionDbContext();

        // GET: Galleries
        public ActionResult Index()
        {
            var galleries = db.galleries
                              .Include(g => g.Categories)
                              .ToList();

            return View(galleries);
        }

        public ActionResult Gallery(int? categoryId)
        {
            ViewBag.Categories = db.collections.ToList();

            var images = db.galleries
                           .Include(g => g.Categories)
                           .Where(g => g.IsActive);

            if (categoryId.HasValue)
            {
                images = images.Where(g => g.CategoryId == categoryId.Value);
            }

            return View(images.ToList());
        }
        // GET: Galleries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // GET: Galleries/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.collections, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Galleries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gallery galleries,
                           HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(ImageFile.FileName);

                    string path = Path.Combine(Server.MapPath("~/Uploads"),
                                               fileName);

                    ImageFile.SaveAs(path);

                    galleries.FileName = fileName;
                    galleries.UploadDate = DateTime.Now;
                    galleries.IsActive = true;
                    
                    db.galleries.Add(galleries);

                    db.SaveChanges();

                    TempData["Success"] =
                        "Image uploaded successfully.";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Please select an image.");
            }

            return View(galleries);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.collections, "CategoryId", "CategoryName", gallery.CategoryId);
            return View(gallery);
        }

        // POST: Galleries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageId,ImageTitle,ImageDescription,FileName,CategoryId")] Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.collections, "CategoryId", "CategoryName", gallery.CategoryId);
            return View(gallery);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery gallery = db.galleries.Find(id);
            db.galleries.Remove(gallery);
            db.SaveChanges();
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
