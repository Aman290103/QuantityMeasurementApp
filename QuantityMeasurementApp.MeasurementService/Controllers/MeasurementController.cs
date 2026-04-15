using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Entity;
using QuantityMeasurementApp.Service;

namespace QuantityMeasurementApp.MeasurementService.Controllers
{
    [ApiController]
    [Route("api/v1/measurement")]
    public class MeasurementController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;

        public MeasurementController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        [HttpPost("compare")]
        public ActionResult<QuantityMeasurementDTO> Compare([FromBody] QuantityInputDTO input)
        {
            return Ok(_service.CompareRest(input));
        }

        [HttpPost("convert")]
        public ActionResult<QuantityMeasurementDTO> Convert([FromBody] QuantityInputDTO input)
        {
            return Ok(_service.ConvertRest(input));
        }

        [HttpPost("add")]
        public ActionResult<QuantityMeasurementDTO> Add([FromBody] QuantityInputDTO input)
        {
            return Ok(_service.AddRest(input));
        }

        [HttpPost("subtract")]
        public ActionResult<QuantityMeasurementDTO> Subtract([FromBody] QuantityInputDTO input)
        {
            return Ok(_service.SubtractRest(input));
        }
    }
}
