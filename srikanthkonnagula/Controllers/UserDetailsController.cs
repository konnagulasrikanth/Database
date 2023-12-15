using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using srikanthkonnagula.Models;
using System.Runtime.InteropServices;

namespace KonnagulaDatabase.Controllers
{

    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        TrainingManagementSystemContext dc = new TrainingManagementSystemContext();
        [Route("api/db4")]
        [HttpGet]
        public IEnumerable<UserDetails> GetUserDetails()
        {
            var res = dc.UserDetails.Select(t => t);
            return res.ToList();
        }

        [Route("user/getu")]

        [HttpGet]

        public IActionResult Getu()

        {

            var users = dc.UserDetails

                .Select(user => new { user.UserName, user.Password })

                .ToList();

            return Ok(users);

        }

        [HttpPost]
        [Route("login")]
        public ActionResult<object> Login(UserDetails user)
        {
            var existingUser = dc.UserDetails.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password && u.UserType == user.UserType);
            if (existingUser != null)
            {
                switch (user.UserType)
                {
                    case "Employee":
                        var employeeId = GetEmployeeIdByUsername(existingUser.UserName);

                        if (employeeId != null)
                        {
                            HttpContext.Session.SetInt32("employeeId", Convert.ToInt32(employeeId.Value));

                            var employee = dc.Employee.FirstOrDefault(e => e.EmployeeName == existingUser.UserName);
                            if (employee != null)
                            {
                                return new { Message = "Authentication successful!", EmployeeId = employee.EmployeeId, UserType = "Employee" };
                            }
                            else
                            {
                                return BadRequest("EmployeeId not found for the given username.");
                            }
                        }
                        else
                        {
                            return BadRequest("EmployeeId not found for the given username.");
                        }
                    case "Manager":
                        // Additional logic for Manager login
                        return new { Message = "Authentication successful!", UserType = "Manager" };
                    case "Hr":
                        // Additional logic for HR login
                        return new { Message = "Authentication successful!", UserType = "Hr" };
                    default:
                        return BadRequest("Invalid userType");
                }
            }
            else
            {
                return Unauthorized("Invalid credentials");
            }
        }
        [HttpGet("api/empbyusername/{employeeName}")]
        public ActionResult<int?> GetEmployeeIdByUsername(string employeeName)
        {
            try
            {
                var employee = dc.Employee.FirstOrDefault(e => e.EmployeeName == employeeName);

                if (employee != null)
                {
                    return Ok(employee.EmployeeId);
                }
                else
                {
                    return NotFound($"No employee found for username: {employeeName}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [Route("api/userbyid")]
        [HttpGet]
        public IActionResult GetUserDetails(int id)
        {
            var res = dc.UserDetails.Where(t => t.UserId == id).Select(t => t);
            if (res.Count() > 0)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
        [Route("api/adduser")]
        [HttpPost]
        public String AddUserDetails(UserDetails ud)
        {
            try
            {
                dc.UserDetails.Add(ud);
                dc.SaveChanges();
                return (ud.UserId.ToString());
            }
            catch
            (Exception e)
            {
                return e.Message;

            }

        }


        [Route("api/edituser")]
        [HttpPut]
        public string EditUserDetails(UserDetails ud)
        {
            dc.UserDetails.Update(ud);
            int i = dc.SaveChanges();
            return "no of rows affected :" + i;
        }
        [Route("api/deleteuser")]
        [HttpDelete]
        public string deleteUserDetails(int id)
        {

            var res = dc.UserDetails.Where(t => t.UserId == id).Select(t => t).FirstOrDefault();
            dc.UserDetails.Remove(res);
            int i = dc.SaveChanges();
            return "no of rows affected : " + i;
        }
    }
}
