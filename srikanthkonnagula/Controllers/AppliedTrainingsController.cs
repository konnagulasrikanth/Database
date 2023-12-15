using srikanthkonnagula.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace KonnagulaDatabase.Controllers
{
    [ApiController]
    public class AppliedTrainingsController : ControllerBase
    {
        TrainingManagementSystemContext dc = new TrainingManagementSystemContext();
        [Route("api/appendAT")]
        [HttpPost]
        public IActionResult AppendAT(AppliedTrainings appliedTrainings)
        {
            dc.AppliedTrainings.Add(appliedTrainings);
            int i = dc.SaveChanges();
            if (i > 0)
            {
                return Ok("AT added successfully");
            }
            else
            {
                return BadRequest("AT details aren't added");
            }
        }
    }
}