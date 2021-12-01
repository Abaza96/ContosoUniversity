using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolContext _context;

        public HomeController(ILogger<HomeController> logger, SchoolContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            IQueryable<EnrollmentDateGroup> Data =
                from student in _context.Students
                group student by student.EnrollmentDate into DateGroup
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = DateGroup.Key,
                    StudentsCount = DateGroup.Count()
                };

            /*
             * Chaining Method
             _context.Students.GroupBy(q => q, q => q.EnrollmentDate, k,q => new EnrollmentDateGroup()
                {
                    EnrollmentDate = DateGroup.Key,
                    StudentsCount = DateGroup.Count()
                });
             */
            return View(Data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
