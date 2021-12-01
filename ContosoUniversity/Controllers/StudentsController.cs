using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.FNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FName_desc" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "Name_asc";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.SearchFilter = searchString;

            var students = _context.Students.Select(q => q);
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstName.ToUpper().Contains(searchString.ToUpper()));
            }
            students = sortOrder switch
            {
                "Name_asc" => students.OrderBy(s => s.LastName),
                "Name_desc" => students.OrderByDescending(s => s.LastName),
                "FName_desc" => students.OrderByDescending(s => s.FirstName),
                "Date" => students.OrderBy(s => s.EnrollmentDate),
                "Date_desc" => students.OrderByDescending(s => s.EnrollmentDate),
                _ => students.OrderBy(s => s.FirstName),
            };
            return View(await students.AsNoTracking().ToListAsync());
        }
        public IActionResult AllStudents() => View(_context.Students.ToList());
        

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(s => s.Course)
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        /*
         =>
                (
                    id == null ? 
                        NotFound() :
                        (
                            await _context.Students.Include(s => s.Enrollments).ThenInclude(s => s.Course).FirstOrDefaultAsync(m => m.StudentID == id) == null ? 
                                NotFound() : 
                                View(await _context.Students.Include(s => s.Enrollments).ThenInclude(s => s.Course).FirstOrDefaultAsync(m => m.StudentID == id))
                        )
                );
        */

        // GET: Students/Create
        public IActionResult Create() => View();
        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,FirstName,LastName,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to Save Changes, Try again and See if the problem presists");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
                if (student == null)
                {
                    return NotFound();
                }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,FirstName,LastName,EnrollmentDate")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChngeError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }
            if (saveChngeError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Deleting Failed, try again";
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")] //ActionName() is a method that relates this method with the passed param
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("Delete", new { id, saveChangesError = true });
            }
        }
        private bool StudentExists(int id) => _context.Students.Any(e => e.StudentID == id);
    }
}
