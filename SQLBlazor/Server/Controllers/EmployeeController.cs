using Microsoft.AspNetCore.Mvc;
using SQLBlazor.Server.Models;
using SQLBlazor.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace SQLBlazor.Server.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private TestingContext _context;

        public EmployeeController(TestingContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public ActionResult<List<Employee>> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeebyid(int id)
        {
            return _context.Employees.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost("")]
        public Employee ReturnEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        [HttpPut("{id}")]
        public Employee PutEmployee(int id, Employee newEmployee)
        {
            Employee oldEmployee = _context.Employees.FirstOrDefault(x => x.Id == id);
            oldEmployee.EmpFirstname = newEmployee.EmpFirstname;
            oldEmployee.EmpSecondname = newEmployee.EmpSecondname;
            oldEmployee.EmpEmailAddress = newEmployee.EmpEmailAddress;

            _context.SaveChanges();

            return oldEmployee;
        }

        [HttpDelete("")]
        public int DeleteEmployee(int id)
        {
            Employee oldEmployee = _context.Employees.FirstOrDefault(x => x.Id == id);
            _context.Employees.Remove(oldEmployee);

            _context.SaveChanges();

            return id;
        }
    }
}
