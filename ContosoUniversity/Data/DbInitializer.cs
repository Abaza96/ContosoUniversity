using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ContosoUniversity.Models.Enrollment;

namespace ContosoUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();
            if (context.Students.Any())
            {
                return;
            }

            Student[] students = new Student[]
            {
                new Student { LastName = "Mahfouz", FirstName = "Boshkash", EnrollmentDate = DateTime.Parse("1-1-2020") },
                new Student { LastName = "Helal", FirstName = "Roushdy", EnrollmentDate = DateTime.Parse("1-1-2020") },
                new Student { LastName = "Al-Koumy", FirstName = "Bakr", EnrollmentDate = DateTime.Parse("1-1-2020") },
                new Student { FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student { FirstName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student { FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student { FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student { FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student { FirstName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student { FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student { FirstName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };

            Course[] courses = new Course[]
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3,},
            new Course{CourseID=4022,Title="Microeconomics",Credits=3,},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3,},
            new Course{CourseID=1045,Title="Calculus",Credits=4,},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4,},
            new Course{CourseID=2021,Title="Composition",Credits=3,},
            new Course{CourseID=2042,Title="Literature",Credits=4,}
            };

            Enrollment[] enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grades.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grades.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grades.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grades.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grades.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grades.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050,},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grades.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grades.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grades.A},
            };

            students.ToList().ForEach(q => context.Students.Add(q));
            context.SaveChanges();

            courses.ToList().ForEach(q => context.Courses.Add(q));
            context.SaveChanges();

            enrollments.ToList().ForEach(q => context.Enrollments.Add(q));
            context.SaveChanges();
        }
    }
}
