using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject1.ViewModels.School;

namespace SchoolProject1.Controllers
{
    public class SchoolController : Controller
    {
        private readonly SchoolDbContext context;

        public SchoolController(SchoolDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<School> schools=await context.Schools.Where(x=>!x.IsDeleted).ToListAsync();
            var model=new List<SchoolIndexViewModel>();
            foreach (var school in schools)
            {
                SchoolIndexViewModel modelItem = new SchoolIndexViewModel
                {
                    Id = school.Id,
                    Name = school.Name,
                    SchoolAddress=school.SchoolAddress
                };
                model.Add(modelItem);
            }
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            School school=await context.Schools.FindAsync(id);
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
                CreatedOn=DateTime.Now
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
                CreatedOn=DateTime.Now,
                IsDeleted=false
                };
                context.Schools.Add(school);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            School school = await context.Schools.FindAsync(id);
            if(school == null)
            {
                return NotFound();
            }
            SchoolEditViewModel model=new SchoolEditViewModel { 
            Id = id,
            Name = school.Name,
            SchoolAddress=school.SchoolAddress
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
                School school = await context.Schools.FindAsync(id);
                if (school == null)
                {
                    return NotFound();
                }
                school.Name = model.Name;
                school.SchoolAddress = model.SchoolAddress;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            School school = await context.Schools.FindAsync(id);
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
            School school1 = await context.Schools.FindAsync(school.Id);
            if(school1 == null)
                { return NotFound(); }
            school1.IsDeleted = true;
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
