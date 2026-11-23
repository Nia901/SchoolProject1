using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolProject1.Core.Interfaces;
using SchoolProject1.Core.Services;
using SchoolProject1.ViewModels.Student;

namespace SchoolProject1.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService serviceStudent;
        private readonly ITeacherService serviceTeacher;

        public StudentController(IStudentService serviceStudent, ITeacherService serviceTeacher)
        {
            this.serviceStudent = serviceStudent;
            this.serviceTeacher = serviceTeacher;
        }
        public async Task<IActionResult> Index()
        {
            List<Student> students1=await serviceStudent.GetAllStudentsAsync(); 
            List<StudentIndexViewModel> students = new List<StudentIndexViewModel>();
            foreach (var student in students1)
            {
                StudentIndexViewModel model= new StudentIndexViewModel { 
                    Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age
                };
                students.Add(model);
            }
            return View(students);
        }
        private async Task LoadTeachers(List<int> selectedTeachers = null,Student s=null)
        {
            var teacherQuery = await serviceTeacher.GetAllTeachersAsync();

            var teachers = teacherQuery
                .Select(d => new
                {
                    d.Id,
                    FullName = d.FirstName + " " + d.LastName
                })
                .ToList();

            ViewBag.Teachers = new MultiSelectList(
                teachers,
                "Id",
                "FullName",
                selectedTeachers
            );

        }
        public async Task<IActionResult> Create()
        {
            await LoadTeachers();
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
                    PhoneNumber = model.PhoneNumber
                };
                foreach (var teacherId in model.TeacherIds)//direktno 
                {
                    s.StudentTeacher.Add(new StudentTeacher
                    {
                        TeacherId = teacherId
                    });
                }
                Teacher t = await serviceTeacher.GetTeacherByIdAsync(model.TeacherIds.First());
                int schoolId = t.SchoolId;
                await serviceStudent.AddStudentAsync(s, schoolId);

                return RedirectToAction(nameof(Index));
            }
            await LoadTeachers();
            return View(model);
        }
        public async Task<IActionResult>Edit(int id)
        {
            Student st = await serviceStudent.GetStudentByIdAsync(id);
            if (st == null)
                return NotFound();
            StudentEditViewModel model=new StudentEditViewModel { 
            Id= id,
            FirstName= st.FirstName,
            LastName= st.LastName,
            Age= st.Age,
            Address = st.Address,
            Email = st.Email,
            PhoneNumber = st.PhoneNumber
            };

            var selectedTeachers = st.StudentTeacher
    .Select(x => x.TeacherId)
    .ToList();

            await LoadTeachers(selectedTeachers);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,StudentEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (model.TeacherIds.Count == 0)
                return NotFound();
            int oldTeach = -1;
            if(ModelState.IsValid)
            {
                Student st=await serviceStudent.GetStudentByIdAsync(model.Id);
               if(st==null)
                    return NotFound();
                StudentTeacher stt = st.StudentTeacher.FirstOrDefault(t => t.StudentId == st.Id);
                if(stt!=null)
                oldTeach = stt.TeacherId;
               st.FirstName = model.FirstName;
                st.LastName = model.LastName;
                st.Age = model.Age;
                st.Address = model.Address;
                    st.Email = model.Email;
                st.PhoneNumber = model.PhoneNumber;
                st.StudentTeacher.Clear();
                bool t = false;
                int teacherId1 = -1;
                string schoolName = (await serviceTeacher.GetTeacherByIdAsync(model.TeacherIds[0])).School.Name;
                foreach (var teacherId in model.TeacherIds)//direktno 
                {
                    if((await serviceTeacher.GetTeacherByIdAsync(teacherId)).School.Name != schoolName)
                    {
                        t = true;
                        teacherId1 = -1;
                        break;
                    }
                    st.StudentTeacher.Add(new StudentTeacher
                    {
                        TeacherId = teacherId
                    });
                    teacherId1 = teacherId;
                }
                if (!t)
                {
                    await serviceStudent.UpdateStudentAsync(teacherId1, oldTeach);
                }
                
                return RedirectToAction(nameof(Index));
            }
            await LoadTeachers(model.TeacherIds);
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Student st = await serviceStudent.GetStudentByIdAsync(id);
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
            Student st = await serviceStudent.GetStudentByIdAsync(model.Id);
            if(st == null)
            {
                return NotFound();
            }
            int? schoolId=null;
            if (st.StudentTeacher.FirstOrDefault() != null)
            {
                schoolId = st.StudentTeacher.FirstOrDefault().Teacher.SchoolId;
            }
            await serviceStudent.DeleteStudentAsync(st.Id, schoolId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Details(int id)
        {
            Student st = await serviceStudent.GetStudentByIdAsync(id);
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
                PhoneNumber=st.PhoneNumber,
                TeacherName=string.Join(",",st.StudentTeacher.Select(t=>t.Teacher.FirstName+" "+t.Teacher.LastName))
            };
            return View(model);
        }
    }
}
