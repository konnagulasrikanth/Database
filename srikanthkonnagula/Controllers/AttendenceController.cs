using srikanthkonnagula.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KonnagulaDatabase.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : ControllerBase
    {
        TrainingManagementSystemContext dc = new TrainingManagementSystemContext();
        [Route("api/db5")]
        [HttpGet]
        public IEnumerable<Attendence> GetAttendence()
        {
            var res = dc.Attendence.Select(t => t);
            return res.ToList();
        }
        [Route("api/attbyuserid")]
        [HttpGet]
        public IActionResult GetUserAttendance(int id)
        {
            var res = dc.Attendence.Where(t => t.AttendenceId == id).Select(t => t);
            if (res.Count() > 0)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
        [Route("api/attadduser")]
        [HttpPost]
        public int AddUserDetails(Attendence att)
        {
            dc.Attendence.Add(att);
            return dc.SaveChanges();
        }
        [Route("api/attedituser")]
        [HttpPut]
        public string EditUserDetails(Attendence att)
        {
            dc.Attendence.Update(att);
            int i = dc.SaveChanges();
            return "no of rows affected :" + i;
        }
        [Route("api/attdeleteuser")]
        [HttpDelete]
        public string deleteUserDetails(int id)
        {

            var res = dc.Attendence.Where(t => t.AttendenceId == id).Select(t => t).FirstOrDefault();
            dc.Attendence.Remove(res);
            int i = dc.SaveChanges();
            return "no of rows affected : " + i;
        }
    }
}