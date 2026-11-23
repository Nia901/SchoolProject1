using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolProject1.Core.Interfaces;
using SchoolProject1.ViewModels.Student;
using SchoolProject1.ViewModels.Teacher;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolProject1.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ISchoolService serviceSchool;
        private readonly ITeacherService serviceTeacher;

        public TeacherController(ISchoolService serviceSchool, ITeacherService serviceTeacher)
        {
            this.serviceSchool = serviceSchool;
            this.serviceTeacher = serviceTeacher;
        }
        private async Task LoadSchools(Teacher t=null)
        {
            var schools = await serviceSchool.GetAllSchoolsAsync();

            ViewBag.Schools = new SelectList(
                schools,
                "Id",
                "Name"
            );

        }
        [HttpGet]
        public async Task<IActionResult> GetFilterTeachers(string? name, string? subject, string? sortOrder)
        {
            var teachers = await serviceTeacher.GetAllTeachersAsync(name,subject, sortOrder);

            var result = teachers.Select(d => new
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Age = d.Age,
                Email = d.Email,
                Subject = d.Subject,
                PhoneNumber = d.PhoneNumber,
                Address = d.Address,
                SchoolName = d.School.Name
            }).ToList();
            return Json(result);
        }
        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers =await serviceTeacher.GetAllTeachersAsync();
            List<TeacherIndexViewModel> models = new List<TeacherIndexViewModel>();
            foreach (var teacher in teachers)
            {
                TeacherIndexViewModel model = new TeacherIndexViewModel
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Age = teacher.Age,
                    Email=teacher.Email,
                    Subject = teacher.Subject,
                    PhoneNumber =teacher.PhoneNumber,
                    Address=teacher.Address,
                    SchoolName=teacher.School.Name
                };
               models.Add(model);
            }
            return View(models);
        }
       
        public async Task<IActionResult> Create()
        {
            await LoadSchools();
            return View();
        }
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create(TeacherCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Teacher teacher = new Teacher
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Subject=model.Subject,
                    YearsOfExperience = model.YearsOfExperience,
                    SchoolId = model.SchoolId
                };
                await serviceTeacher.AddTeacherAsync(teacher);

                return RedirectToAction(nameof(Index));
            }

            await LoadSchools();
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Teacher t = await serviceTeacher.GetTeacherByIdAsync(id);
            if (t == null)
                return NotFound();
            TeacherEditViewModel model = new TeacherEditViewModel
            {
                Id = t.Id,
                FirstName =t.FirstName,
                LastName=t.LastName,
                Age=t.Age,
                Address=t.Address,
                Email=t.Email,
                PhoneNumber=t.PhoneNumber,
                Subject = t.Subject,
                YearsOfExperience =t.YearsOfExperience,
                SchoolId=t.SchoolId
            };
            await LoadSchools();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TeacherEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                Teacher t=await serviceTeacher.GetTeacherByIdAsync(id);
                if (t == null)
                    return NotFound();
                t.FirstName = model.FirstName;
                t.YearsOfExperience = model.YearsOfExperience;
                t.LastName = model.LastName;
                t.Age = model.Age;
                t.Address = model.Address;
                t.Email = model.Email;
                t.PhoneNumber = model.PhoneNumber;
                t.Subject = model.Subject;
                t.SchoolId = model.SchoolId;
                await serviceTeacher.UpdateTeacherAsync(t);
                return RedirectToAction(nameof(Index));
            }
            await LoadSchools();
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Teacher t = await serviceTeacher.GetTeacherByIdAsync(id);
            if (t == null)
            {
                return NotFound();
            }

            TeacherDeleteViewModel model = new TeacherDeleteViewModel
            {
                Id = id,
                FirstName = t.FirstName,
                LastName = t.LastName
            };
            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(TeacherDeleteViewModel model)
        {
            Teacher t = await serviceTeacher.GetTeacherByIdAsync(model.Id);
            if (t == null)
            {
                return NotFound();
            }
            await serviceTeacher.DeleteTeacherAsync(t.Id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            Teacher t = await serviceTeacher.GetTeacherByIdAsync(id);
            if (t == null)
            {
                return NotFound();
            }
            TeacherDetailsViewModel model = new TeacherDetailsViewModel
            {
                Id = id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Age = t.Age,
                YearsOfExperience=t.YearsOfExperience,
                Address = t.Address,
                Email = t.Email,
                Subject = t.Subject,
                PhoneNumber = t.PhoneNumber,
                Students = string.Join(", ",
    t.StudentTeacher.Select(x =>
        x.Student.FirstName + " " + x.Student.LastName))
            };
            return View(model);
        }
    }
 
}
