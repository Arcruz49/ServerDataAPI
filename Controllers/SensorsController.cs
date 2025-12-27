using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Globalization;

namespace ServerDataAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorsController : ControllerBase
    {
        [HttpGet("GetTemperature")]
        public IActionResult GetTemperature()
        {
            try
            {
                var tempRaw = System.IO.File.ReadAllText("/sys/class/thermal/thermal_zone2/temp");
                double tempC = double.Parse(tempRaw) / 1000.0;

                return Ok(new { temperature = tempC });
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("CheckStatus")]
        public IActionResult CheckStatus()
        {
            return Ok("Server Online");
        }

        [HttpGet("RamUsage")]
        public IActionResult RamUsage()
        {
            try
            {
                var memInfo = System.IO.File.ReadAllLines("/proc/meminfo");
                long totalMem = 0;
                long freeMem = 0;
                long availableMem = 0;

                foreach (var line in memInfo)
                {
                    if (line.StartsWith("MemTotal:"))
                        totalMem = ParseMemLine(line);
                    else if (line.StartsWith("MemAvailable:"))
                        availableMem = ParseMemLine(line);
                }

                long usedMem = totalMem - availableMem;

                return Ok(new
                {
                    totalMB = totalMem / 1024,
                    usedMB = usedMem / 1024,
                    freeMB = availableMem / 1024,
                    usedPercent = Math.Round((double)usedMem / totalMem * 100, 2)
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        private long ParseMemLine(string line)
        {
            var parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return long.Parse(parts[1], CultureInfo.InvariantCulture); 
        }
    }
}
