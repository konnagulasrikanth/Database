using srikanthkonnagula.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KonnagulaDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRController : ControllerBase
    {
        TrainingManagementSystemContext dc = new TrainingManagementSystemContext();
        [Route("api/db2")]
        [HttpGet]
        public IEnumerable<TrainingData> GetTrainingData()
        {
            var res = dc.TrainingData.Select(t => t);
            return res.ToList();
        }
        [Route("api/trngbyid")]
        [HttpGet]
        public IActionResult GetTrainingData(int id)
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
        [Route("api/addTrngdata")]
        [HttpPost]
        public int AddTrainingData(TrainingData trng)
        {

            dc.TrainingData.Add(trng);
            // var res = dc.Employees.Select(t => t);
            return dc.SaveChanges();
        }
        [Route("api/edit")]
        [HttpPut]
        public string EditTrainingData(TrainingData trng)
        {
            dc.TrainingData.Update(trng);
            int i = dc.SaveChanges();
            return "no of rows affected :" + i;
        }
        [Route("api/delete")]
        [HttpDelete]
        public string deleteTrainingData(int id)
        {

            var res = dc.TrainingData.Where(t => t.TrainingId == id).Select(t => t).FirstOrDefault();
            dc.TrainingData.Remove(res);
            int i = dc.SaveChanges();
            return "no of rows affected : " + i;
        }
    }
}