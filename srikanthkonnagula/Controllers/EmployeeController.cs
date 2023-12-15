using srikanthkonnagula.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace KonnagulaDatabase.Controllers
{

    [ApiController]
    public class EmployeeController : ControllerBase

    {

        TrainingManagementSystemContext dc = new TrainingManagementSystemContext();

        [Route("api/db")]

        [HttpGet]

        //[EnableQuery(PageSize =3)]

        public IEnumerable<Employee> Getemployee()

        {

            /* var res = from t in dc.Employees

                       select t;*/

            var res = dc.Employee.Select(t => t);

            return res.ToList();

        }

        //[Route("db1")]

        //[HttpGet]

        [Route("api/empbyid")]

        [HttpGet]
        public IActionResult Getemployee(int id)

        {

            /*var res = from t in dc.Employees

                      where t.Empid==id

                       select t;*/

            var res = dc.Employee.Where(t => t.EmployeeId == id).Select(t => t);

            if (res.Count() > 0)

            {

                return Ok(res);

            }

            else

            {

                return NotFound();

            }

        }

        [Route("api/addemp")]

        [HttpPost]

        //[FromQuery]

        public int Addemployee(Employee emp)

        {

            dc.Employee.Add(emp);

            // var res = dc.Employees.Select(t => t);

            return dc.SaveChanges();

        }

        [Route("api/edit")]

        [HttpPut]

        public string Editemployee(Employee emp)

        {

            dc.Employee.Update(emp);

            int i = dc.SaveChanges();

            return "no of rows affected :" + i;

        }



        [Route("api/delete")]

        [HttpDelete]

        public string deleteemployee(int id)

        {

            var res = dc.Employee.Where(t => t.EmployeeId == id).Select(t => t).FirstOrDefault();

            dc.Employee.Remove(res);

            int i = dc.SaveChanges();

            return "no of rows affected : " + i;

        }
    }
}
