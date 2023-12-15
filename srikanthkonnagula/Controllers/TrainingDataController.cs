using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using srikanthkonnagula.Models;

namespace KonnagulaDatabase.Controllers
{
    [ApiController]
    public class TrainingDataController : ControllerBase
    {
        TrainingManagementSystemContext dc = new TrainingManagementSystemContext();
        [Route("api/db7")]
        [HttpGet]
        public IEnumerable<TrainingData> GetTD()
        {
            var res = dc.TrainingData.Select(t => t);
            return res.ToList();
        }

        [Route("api/tdbyid")]
        [HttpGet]
        public IActionResult GetTD(int id)
        {
            var res = dc.TrainingData.Where(t => t.TrainingId == id).Select(t => t);
            if (res.Count() > 0)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
        [Route("api/addtd")]
        [HttpPost]
        public IActionResult AddTD(TrainingData trngdata)
        {

            dc.TrainingData.Add(trngdata);
            int i = dc.SaveChanges();
            if (i > 0)
            {
                return Ok("tranining data added successfully");
            }
            else
            {
                return BadRequest("invalid details");
            }


        }
        [Route("api/edittd")]
        [HttpPut]
        public string EditTD(TrainingData trngdata)
        {
            dc.TrainingData.Update(trngdata);
            int i = dc.SaveChanges();
            return "no of rows affected :" + i;
        }
        [Route("api/deletetd")]
        [HttpDelete]
        public string deleteTD(int id)
        {

            var res = dc.TrainingData.Where(t => t.TrainingId == id).Select(t => t).FirstOrDefault();
            dc.TrainingData.Remove(res);
            int i = dc.SaveChanges();
            return "no of rows affected : " + i;
        }
    }
}