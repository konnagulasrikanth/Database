using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using srikanthkonnagula.Models;
using Microsoft.EntityFrameworkCore;


namespace KonnagulaDatabase.Controllers
{
    [ApiController]
    public class TrainingRegistrationController : ControllerBase
    {
        TrainingManagementSystemContext dc = new TrainingManagementSystemContext();
        [Route("api/db8")]
        [HttpGet]
        public IEnumerable<TraineeRegistration> GetTraineeReg()
        {
            var res = dc.TraineeRegistration.Select(t => t);
            return res.ToList();
        }

        [Route("api/trbyid")]
        [HttpGet]
        public IActionResult GetTraineeReg(int id)
        {
            /*var res = from t in dc.Employees
                      where t.Empid==id
                       select t;*/
            var res = dc.TraineeRegistration.Where(t => t.TrainingId == id).Select(t => t);
            if (res.Count() > 0)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
        [Route("api/addtr")]
        [HttpPost]
        public IActionResult AddTraineeReg(TraineeRegistration tr)
        {

            dc.TraineeRegistration.Add(tr);
            int i = dc.SaveChanges();
            if (i > 0)
            {
                return Ok("TraineeReg added successfully");
            }
            else
            {
                return BadRequest("invalid details");
            }


        }
        [Route("api/edittr")]
        [HttpPut]
        public string Editemployee(TraineeRegistration tr)
        {
            dc.TraineeRegistration.Update(tr);
            int i = dc.SaveChanges();
            return "no of rows affected :" + i;
        }
        [Route("api/deletetr")]
        [HttpDelete]
        public string DeleteTraineeReg(int id)
        {

            var res = dc.TraineeRegistration.Where(t => t.TrainingId == id).Select(t => t).FirstOrDefault();
            dc.TraineeRegistration.Remove(res);
            int i = dc.SaveChanges();
            return "no of rows affected : " + i;
        }
    }
}
