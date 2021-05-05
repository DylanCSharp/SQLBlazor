using Microsoft.AspNetCore.Mvc;
using SQLBlazor.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace SQLBlazor.Server.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IConfiguration _config;

        public EmployeeController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet]
        public ActionResult<List<Employee>> GetEmployees()
        {
            List<Employee> employees = new();
            using (SqlConnection conn = new(_config.GetConnectionString("BlazorDB")))
            {
                SqlCommand command = new("SP_GetEmployees", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new()
                    {
                        Id = (int)reader["ID"],
                        EmpFirstname = reader["EMP_FIRSTNAME"].ToString(),
                        EmpSecondname = reader["EMP_SECONDNAME"].ToString(),
                        EmpEmailAddress = reader["EMP_EMAIL_ADDRESS"].ToString()
                    };
                    employees.Add(employee);
                }
                conn.Close();
                command.Dispose();
                reader.Close();
            }
            return employees;
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeebyid(int id)
        {
            Employee employee = new();

            using (SqlConnection conn = new(_config.GetConnectionString("BlazorDB")))
            {
                SqlCommand command = new("SP_GetEmployeeByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ID", id);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    employee.Id = (int)reader["ID"];
                    employee.EmpFirstname = reader["EMP_FIRSTNAME"].ToString();
                    employee.EmpSecondname = reader["EMP_SECONDNAME"].ToString();
                    employee.EmpEmailAddress = reader["EMP_EMAIL_ADDRESS"].ToString();
                }
            }
            return employee;
        }

        [HttpPost]
        public Employee ReturnEmployee(Employee employee)
        {
            using (SqlConnection conn = new(_config.GetConnectionString("BlazorDB")))
            {
                SqlCommand command = new("SP_AddEmployee", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FIRSTNAME", employee.EmpFirstname);
                command.Parameters.AddWithValue("@SECONDNAME", employee.EmpSecondname);
                command.Parameters.AddWithValue("@EMAIL", employee.EmpEmailAddress);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            return employee;
        }

        [HttpPut("{id}")]
        public Employee PutEmployee(int id, Employee newEmployee)
        {
            using (SqlConnection conn = new(_config.GetConnectionString("BlazorDB")))
            {
                newEmployee.Id = id;
                SqlCommand command = new("SP_UpdateEmployee", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", newEmployee.Id);
                command.Parameters.AddWithValue("@FIRST_NAME", newEmployee.EmpFirstname);
                command.Parameters.AddWithValue("@SECOND_NAME", newEmployee.EmpSecondname);
                command.Parameters.AddWithValue("@EMAIL", newEmployee.EmpEmailAddress);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            return newEmployee;
        }

        [HttpDelete]
        public int DeleteEmployee(int id)
        {
            using (SqlConnection conn = new(_config.GetConnectionString("BlazorDB")))
            {
                SqlCommand command = new("SP_DeleteEmployee", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            return id;
        }
    }
}
