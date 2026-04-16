using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Service;
using System.Linq;

namespace QuantityMeasurementApp.AnalyticsService.Controllers
{
    [ApiController]
    [Route("api/v1/analytics")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;

        public AnalyticsController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        [HttpGet("popular-units")]
        public IActionResult GetPopularUnits()
        {
            var history = _service.GetAllHistory();
            var popular = history
                .GroupBy(h => h.ThisMeasurementType)
                .Select(g => new { Unit = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count);
                
            return Ok(popular);
        }

        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            var history = _service.GetAllHistory().ToList();
            return Ok(new {
                TotalOperations = history.Count,
                Successful = history.Count(h => !h.Error),
                Failed = history.Count(h => h.Error)
            });
        }
    }
}
