using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolProject1.ViewModels.Student;

namespace SchoolProject1.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolDbContext context;

        public StudentController(SchoolDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Student> students1=await context.Students.Include(x=>x.School).ToListAsync(); 
            List<StudentIndexViewModel> students = new List<StudentIndexViewModel>();
            foreach (var student in students1)
            {
                StudentIndexViewModel model= new StudentIndexViewModel { 
                    Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                SchoolName=student.School.Name
                };
                students.Add(model);
            }
            return View(students);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Schools = new SelectList(
            await context.Schools
         .Where(h => !h.IsDeleted)
         .ToListAsync(),
             "Id",
            "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                Student s = new Student
                {
                    FirstName= model.FirstName,
                    LastName= model.LastName,
                    Age = model.Age,
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    SchoolId=model.SchoolId
                };
                School school=await context.Schools.FindAsync(model.SchoolId);
                
                context.Students.Add(s);
                school.StudentsCount++;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Schools = new SelectList(
                await context.Schools
                    .Where(h => !h.IsDeleted)
                    .ToListAsync(),
                "Id",
                "Name");
            return View(model);
        }
        public async Task<IActionResult>Edit(int id)
        {
            Student st = await context.Students.FindAsync(id);
            if (st == null)
                return NotFound();
            StudentEditViewModel model=new StudentEditViewModel { 
            Id= id,
            FirstName= st.FirstName,
            LastName= st.LastName,
            Age= st.Age,
            Address = st.Address,
            Email = st.Email,
            PhoneNumber = st.PhoneNumber,
            SchoolId = st.SchoolId
            };
            ViewBag.Schools = new SelectList(
                    await context.Schools
                        .Where(h => !h.IsDeleted)
                        .ToListAsync(),
                    "Id",
                    "Name");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,StudentEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                Student st=await context.Students.FindAsync(id);
               if(st==null)
                    return NotFound();
               st.FirstName = model.FirstName;
                st.LastName = model.LastName;
                st.Age = model.Age;
                st.Address = model.Address;
                    st.Email = model.Email;
                st.PhoneNumber = model.PhoneNumber;
                st.SchoolId = model.SchoolId;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Schools = new SelectList(
                 await context.Schools
                     .Where(h => !h.IsDeleted)
                     .ToListAsync(),
                 "Id",
                 "Name");
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Student st = await context.Students.FindAsync(id);
            if (st == null)
            {
                return NotFound();
            }
            
            StudentDeleteViewModel model = new StudentDeleteViewModel
            {
                Id = id,
                FirstName = st.FirstName,
                LastName = st.LastName
            };
            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult>DeleteConfirmed(StudentDeleteViewModel model)
        {
            Student st = await context.Students.FindAsync(model.Id);
            if(st == null)
            {
                return NotFound();
            }
            School school = await context.Schools.FindAsync(st.SchoolId);
            context.Students.Remove(st);
            school.StudentsCount--;
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Details(int id)
        {
            Student st = await context.Students.FindAsync(id);
            if (st == null)
            {
                return NotFound();
            }
            StudentDetailsViewModel model = new StudentDetailsViewModel
            {
                Id=id,
                FirstName=st.FirstName,
                LastName=st.LastName,
                Age=st.Age,
                Address=st.Address,
                Email=st.Email,
                PhoneNumber=st.PhoneNumber
            };
            return View(model);
        }
    }
}
