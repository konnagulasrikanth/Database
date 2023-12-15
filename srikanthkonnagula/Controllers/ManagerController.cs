using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using srikanthkonnagula.Models;

namespace KonnagulaDatabase.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ManagerController : ControllerBase
    {
        TrainingManagementSystemContext dc = new TrainingManagementSystemContext();
        [Route("api/db3")]
        [HttpGet]
        public IEnumerable<ManagerData> GetManagerData()
        {
            var res = dc.ManagerData.Select(t => t);
            return res.ToList();
        }
        [Route("api/mgrdtbyid")]
        [HttpGet]
        public IActionResult GetManagerData(int id)
        {
            var res = dc.ManagerData.Where(t => t.ManagerId == id).Select(t => t);
            if (res.Count() > 0)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
        [Route("api/addmgrdata")]
        [HttpPost]
        public int AddManagerData(ManagerData mgr)
        {

            dc.ManagerData.Add(mgr);
            // var res = dc.Employees.Select(t => t);
            return dc.SaveChanges();
        }
        [Route("api/editmgrdata")]
        [HttpPut]
        public string EditManagerData(ManagerData mgr)
        {
            dc.ManagerData.Update(mgr);
            int i = dc.SaveChanges();
            return "no of rows affected :" + i;
        }
        [Route("api/deletemgrdata")]
        [HttpDelete]
        public string deleteManagerData(int id)
        {

            var res = dc.ManagerData.Where(t => t.ManagerId == id).Select(t => t).FirstOrDefault();
            dc.ManagerData.Remove(res);
            int i = dc.SaveChanges();
            return "no of rows affected : " + i;
        }
    }
}