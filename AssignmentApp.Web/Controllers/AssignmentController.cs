using AssignmentApp.DAL;
using AssignmentApp.Domain;
using AssignmentApp.Services.EmailService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentApp.Web.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AssignmentDbContext _db;
        private IEmailService _emailService;

        public AssignmentController(ILogger<HomeController> logger, AssignmentDbContext db,IEmailService emailService)
        {
            _logger = logger;
            _db = db;
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            return View(_db.AssignmentItems.AsEnumerable());
        }

        public IActionResult Create()
        {
            return View(new AssignmentItem());
        }


        [HttpPost]
        public async Task<IActionResult> Create(AssignmentItem model)
        {
            if (ModelState.IsValid)
            {
                _db.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            return View(_db.AssignmentItems.FirstOrDefault(a => a.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AssignmentItem model)
        {
            if (ModelState.IsValid)
            {
                var editModel = _db.AssignmentItems.Find(model.Id);
                editModel.AssignmentName = model.AssignmentName;
                editModel.IsCompleted = model.IsCompleted;

                if (editModel.IsCompleted)
                {
                    await _emailService.SendEmailAsync("ilknur.aspir2501@gmail.com", "test", "Task Was Completed", $"Task {editModel.Id} Was Completed on {DateTime.Now}");
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            return View(_db.AssignmentItems.FirstOrDefault(a => a.Id == id));
        }

        [HttpPost]
        public IActionResult Delete(AssignmentItem model)
        {
            var deleteModel = _db.AssignmentItems.Find(model.Id);
            _db.Remove(deleteModel);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
