using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Entity;
using QuantityMeasurementApp.Service;
using System.Collections.Generic;

namespace QuantityMeasurementApp.HistoryService.Controllers
{
    [ApiController]
    [Route("api/v1/history")]
    public class HistoryController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;

        public HistoryController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<QuantityMeasurementDTO>> GetAll()
        {
            return Ok(_service.GetAllHistory());
        }

        [HttpGet("operation/{operation}")]
        public ActionResult<IEnumerable<QuantityMeasurementDTO>> GetByOperation(string operation)
        {
            return Ok(_service.GetOperationHistory(operation.ToUpper()));
        }

        [HttpGet("type/{type}")]
        public ActionResult<IEnumerable<QuantityMeasurementDTO>> GetByType(string type)
        {
            return Ok(_service.GetMeasurementsByType(type));
        }

        [HttpDelete("clear")]
        public ActionResult Clear()
        {
            _service.ClearHistory();
            return NoContent();
        }
    }
}
