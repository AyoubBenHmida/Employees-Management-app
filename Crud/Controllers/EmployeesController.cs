using Crud.Data;
using Crud.Models;
using Crud.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Crud.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CrudDbContext crudDbContext;

        public EmployeesController(CrudDbContext crudDbContext)
        {
            this.crudDbContext = crudDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = crudDbContext.Employees.ToList();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
            };
            
            crudDbContext.Employees.Add(employee) ;
            crudDbContext.SaveChanges() ;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id) {
            var employee = crudDbContext.Employees.FirstOrDefault(x => x.Id == id) ;

            if(employee != null) {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id ,
                    Name = employee.Name ,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth= employee.DateOfBirth
                };
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(UpdateEmployeeViewModel model)
        {
            var employee = crudDbContext.Employees.Find(model.Id) ;

            if(employee != null)
            {
                employee.Name = model.Name ;
                employee.Email = model.Email ;
                employee.Salary = model.Salary ;
                employee.DateOfBirth = model.DateOfBirth ;
                employee.Department = model.Department ;

                crudDbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(UpdateEmployeeViewModel model) 
        {
            var employee = crudDbContext.Employees.Find(model.Id);

            if(employee != null)
            {
                crudDbContext.Employees.Remove(employee);
                crudDbContext.SaveChanges();

                return Redirect("Index");
            }

            return RedirectToAction("Index");
        }
    }
}

