using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject1.Core.Interfaces;
using SchoolProject1.ViewModels.School;

namespace SchoolProject1.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ISchoolService service;

        public SchoolController(ISchoolService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index(string? search, int? minStudents, string? sortOrder)
        {
            List<School> schools = await service.GetAllSchoolsAsync(search,minStudents,sortOrder);
            var model=new List<SchoolIndexViewModel>();
            foreach (var school in schools)
            {
                SchoolIndexViewModel modelItem = new SchoolIndexViewModel
                {
                    Id = school.Id,
                    Name = school.Name,
                    SchoolAddress=school.SchoolAddress,
                    StudentsCount=school.StudentsCount,
                    Email=school.Email,
                    PhoneNumber=school.PhoneNumber,
                    RoomsCount=school.RoomsCount
                };
                model.Add(modelItem);
            }
            ViewBag.Search = search;
            ViewBag.MinStudents = minStudents;
            ViewBag.SortOrder = sortOrder;
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            School school=await service.GetSchoolByIdAsync(id);
            if(school == null)
            {
                return NotFound();
            }
            SchoolDetailsViewModel model = new SchoolDetailsViewModel
            {
                Id=id,
                Name=school.Name,
                SchoolAddress = school.SchoolAddress,
                StudentsCount=school.StudentsCount,
                Email=school.Email,
                PhoneNumber=school.PhoneNumber,
                RoomsCount=school.RoomsCount,
                CreatedOn=school.CreatedOn,
                Teachers = string.Join(", ",
    school.Teachers.Select(x =>
        x.FirstName + " " + x.LastName))
            };
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SchoolCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                School school = new School { 
                Name = model.Name,
                SchoolAddress = model.SchoolAddress,
                StudentsCount=0,
                Email=model.Email,
                PhoneNumber=model.PhoneNumber,
                RoomsCount=model.RoomsCount,
                CreatedOn=DateTime.Now,
                IsDeleted=false
                };
                await service.AddSchoolAsync(school);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            School school = await service.GetSchoolByIdAsync(id);
            if(school == null)
            {
                return NotFound();
            }
            SchoolEditViewModel model=new SchoolEditViewModel { 
            Id = id,
            Name = school.Name,
            SchoolAddress=school.SchoolAddress,
            Email=school.Email,
            PhoneNumber=school.PhoneNumber,
            RoomsCount=school.RoomsCount
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,SchoolEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                School school = await service.GetSchoolByIdAsync(id);
                if (school == null)
                {
                    return NotFound();
                }
                school.Name = model.Name;
                school.SchoolAddress = model.SchoolAddress;
                school.Email = model.Email;
                school.PhoneNumber = model.PhoneNumber;
                school.RoomsCount = model.RoomsCount;
                await service.UpdateSchoolAsync(school);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            School school = await service.GetSchoolByIdAsync(id);
            if(school == null)
            {
                return NotFound();
            }
            SchoolDeleteViewModel model = new SchoolDeleteViewModel
            {
                Id=school.Id,
                Name=school.Name
            };
            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(SchoolDeleteViewModel school)
        {
            try
            {
                await service.DeleteSchoolAsync(school.Id);
                return RedirectToAction(nameof(Index));
            }
            catch(InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
