using srikanthkonnagula.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using srikanthkonnagula.Models;

namespace KonnagulaDatabase.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        TrainingManagementSystemContext dc = new TrainingManagementSystemContext();
        [Route("api/db6")]
        [HttpGet]
        public IEnumerable<Feedback> GetFeedback()
        {
            var res = dc.Feedback.Select(t => t);
            return res.ToList();
        }
        [Route("api/fdbyid")]
        [HttpGet]
        public IActionResult GetFBDetails(int id)
        {
            var res = dc.Feedback.Where(t => t.FeedbackId == id).Select(t => t);
            if (res.Count() > 0)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
        [Route("api/addfdb")]
        [HttpPost]
        public int AddUserDetails(Feedback fdb)
        {
            dc.Feedback.Add(fdb);
            return dc.SaveChanges();
        }
        [Route("api/editfdb")]
        [HttpPut]
        public string EditUserDetails(Feedback fdb)
        {
            dc.Feedback.Update(fdb);
            int i = dc.SaveChanges();
            return "no of rows affected :" + i;
        }
        [Route("api/deletefdb")]
        [HttpDelete]
        public string deleteUserDetails(int id)
        {

            var res = dc.Feedback.Where(t => t.FeedbackId == id).Select(t => t).FirstOrDefault();
            dc.Feedback.Remove(res);
            int i = dc.SaveChanges();
            return "no of rows affected : " + i;
        }
    }
}