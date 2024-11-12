using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AplicaciónWeb.Controllers
{
    public class DocumentoReclamacionController : Controller
    {
        // GET: DocumentoReclamacionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DocumentoReclamacionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DocumentoReclamacionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentoReclamacionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DocumentoReclamacionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DocumentoReclamacionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DocumentoReclamacionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DocumentoReclamacionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
