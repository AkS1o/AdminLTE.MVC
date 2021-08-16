using AdminLTE.MVC.Data;
using AdminLTE.MVC.Models;
using AdminLTE.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.MVC.Controllers
{
    public class WorkerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WorkerController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Worker> workersList = _db.Workers.Include(u => u.Community);

            return View(workersList);
        }

        public IActionResult Create(Worker worker)
        {
            WorkerVM workerVM = new WorkerVM()
            {
                Worker = new Worker(),
                CommunitiesSelectList = _db.Communities.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            return View(workerVM);
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(WorkerVM workerVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                string upload = webRootPath + ENV.ImagePath;
                string filename = Guid.NewGuid().ToString();
                string extentions = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, filename + extentions), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                workerVM.Worker.Image = filename + extentions;

                _db.Workers.Add(workerVM.Worker);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(workerVM);
        }

        public IActionResult Edit()
        {
            WorkerSelectListVM workerSelectListVM = new WorkerSelectListVM()
            {
                WorkerId = null,
                Worker = _db.Workers.Find(null),
                WorkersSelectList = _db.Workers.Select(w => new SelectListItem
                {
                    Text = w.FirstName + " " + w.LastName + " " + w.Patronymic,
                    Value = w.Id.ToString()
                }),
            };

            return View(workerSelectListVM);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(WorkerSelectListVM _workerSelectListVM, string action)
        {
            if (action == "Edit")
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                var worker = _db.Workers.AsNoTracking().FirstOrDefault(w => w.Id == _workerSelectListVM.Worker.Id);

                if (files.Count() > 0)
                {
                    string upload = webRootPath + ENV.ImagePath;
                    string filename = Guid.NewGuid().ToString();
                    string extentions = Path.GetExtension(files[0].FileName);

                    if (worker.Image != null)
                    {
                        var oldFile = Path.Combine(upload, worker.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(upload, filename + extentions), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    _workerSelectListVM.Worker.Image = filename + extentions;
                }
                else
                {
                    _workerSelectListVM.Worker.Image = worker.Image;
                }

                _db.Workers.Update(_workerSelectListVM.Worker);
                _db.SaveChanges();

                return RedirectToAction("Edit");
            }
            else if (action == "Select")
            {
                WorkerSelectListVM workerSelectListVM = new WorkerSelectListVM()
                {
                    WorkerId = _workerSelectListVM.WorkerId,
                    Worker = _db.Workers.Include(w => w.Community).FirstOrDefault(w => w.Id == _workerSelectListVM.WorkerId),
                    WorkersSelectList = _db.Workers.Select(w => new SelectListItem
                    {
                        Text = w.FirstName + " " + w.LastName + " " + w.Patronymic + " Select",
                        Value = w.Id.ToString()
                    }),
                    CommunitiesSelectList = _db.Communities.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                };

                return View("Edit", workerSelectListVM);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Delete()
        {
            WorkerSelectListVM workerSelectListVM = new WorkerSelectListVM()
            {
                WorkerId = null,
                Worker = _db.Workers.Find(null),
                WorkersSelectList = _db.Workers.Select(w => new SelectListItem
                {
                    Text = w.FirstName + " " + w.LastName + " " + w.Patronymic,
                    Value = w.Id.ToString()
                }),
            };

            return View(workerSelectListVM);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(WorkerSelectListVM _workerSelectListVM, string action)
        {
            if (action == "Delete")
            {
                var worker = _db.Workers.Find(_workerSelectListVM.Worker.Id);

                if (worker == null)
                    return NotFound();

                if (worker.Image != null)
                {
                    string upload = _webHostEnvironment.WebRootPath + ENV.ImagePath;
                    var oldFile = Path.Combine(upload, worker.Image);

                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                }

                _db.Workers.Remove(worker);
                _db.SaveChanges();

                return RedirectToAction("Delete");
            }
            else if (action == "Select")
            {
                WorkerSelectListVM workerSelectListVM = new WorkerSelectListVM()
                {
                    WorkerId = _workerSelectListVM.WorkerId,
                    Worker = _db.Workers.Include(w => w.Community).FirstOrDefault(w => w.Id == _workerSelectListVM.WorkerId),
                    WorkersSelectList = _db.Workers.Select(w => new SelectListItem
                    {
                        Text = w.FirstName + " " + w.LastName + " " + w.Patronymic + " Select",
                        Value = w.Id.ToString()
                    }),
                };

                return View("Delete", workerSelectListVM);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
