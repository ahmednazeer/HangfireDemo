using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfire.Api.Dtos
{
    public class AddDriverDto
    {
        public string Name { get; set; }
        public int DriverNumber { get; set; }
        public int Status { get; set; }
    }
}