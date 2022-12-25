using Microsoft.AspNetCore.Mvc;
using Hangfire.Api.Entities;
using Hangfire.Api.Dtos;
using Hangfire.Api.Services;

namespace Hangfire.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {
        private static List<Driver> _drivers = new List<Driver>() { new Driver { Name = "Ahmed Nazeer", Id = Guid.NewGuid(), Status = 1, DriverNumber = 1 } };

        [HttpGet]
        public IActionResult GetDriver(string id){
            var driver= _drivers.SingleOrDefault(d=>d.Id.ToString().Equals(id));
            if(driver==null)
                return NotFound();
            return Ok(driver);
        }
        [HttpGet("list")]
        public IActionResult GetDrivers()
        {
            
            return Ok(_drivers);
        }
        [HttpDelete]
        public IActionResult DeleteDriver(Guid id){
            var driver= _drivers.SingleOrDefault(d=>d.Id==id);
            if(driver==null)
                return NotFound();
            driver.Status=0;//deleted
            return NoContent();
        }

        [HttpPost]
        public IActionResult AddDriver(AddDriverDto driverToAdd){
            var driver= new Driver{Name=driverToAdd.Name,Status=driverToAdd.Status,DriverNumber=driverToAdd.DriverNumber};
            _drivers.Add(driver);
            var jobId = BackgroundJob.Enqueue<IServiceManagement>(ser => ser.SyncData());//run once
            driver.Status=1;//active
            return Created("",driver);
        }

    }
}