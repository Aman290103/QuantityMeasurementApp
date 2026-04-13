using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Entity;
using QuantityMeasurementApp.Service;
using System.Collections.Generic;

namespace QuantityMeasurementApp.WebApi.Controllers
{
    [ApiController]
    // [Authorize]
    [Route("api/v1/quantities")]
    public class QuantityMeasurementApiController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementApiController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        [HttpPost("compare")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), 200)]
        [ProducesResponseType(400)]
        public ActionResult<QuantityMeasurementDTO> CompareQuantities([FromBody] QuantityInputDTO input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = _service.CompareRest(input);
            return Ok(result);
        }

        [HttpPost("convert")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), 200)]
        [ProducesResponseType(400)]
        public ActionResult<QuantityMeasurementDTO> ConvertQuantities([FromBody] QuantityInputDTO input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _service.ConvertRest(input);
            return Ok(result);
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), 200)]
        [ProducesResponseType(400)]
        public ActionResult<QuantityMeasurementDTO> AddQuantities([FromBody] QuantityInputDTO input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _service.AddRest(input);
            return Ok(result);
        }

        [HttpPost("subtract")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), 200)]
        [ProducesResponseType(400)]
        public ActionResult<QuantityMeasurementDTO> SubtractQuantities([FromBody] QuantityInputDTO input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _service.SubtractRest(input);
            return Ok(result);
        }

        [HttpPost("divide")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), 200)]
        [ProducesResponseType(400)]
        public ActionResult<QuantityMeasurementDTO> DivideQuantities([FromBody] QuantityInputDTO input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _service.DivideRest(input);
            return Ok(result);
        }

        [HttpGet("history/operation/{operation}")]
        public ActionResult<IEnumerable<QuantityMeasurementDTO>> GetHistory(string operation)
        {
            return Ok(_service.GetOperationHistory(operation.ToUpper()));
        }

        [HttpGet("history/type/{type}")]
        public ActionResult<IEnumerable<QuantityMeasurementDTO>> GetByType(string type)
        {
            return Ok(_service.GetMeasurementsByType(type));
        }

        [HttpGet("count/{operation}")]
        public ActionResult<int> GetOperationCount(string operation)
        {
            return Ok(_service.GetOperationCount(operation.ToUpper()));
        }

        [HttpGet("history/errored")]
        public ActionResult<IEnumerable<QuantityMeasurementDTO>> GetErroredHistory()
        {
            return Ok(_service.GetErrorHistory());
        }

        [HttpGet("history/all")]
        public ActionResult<IEnumerable<QuantityMeasurementDTO>> GetAllHistory()
        {
            return Ok(_service.GetAllHistory());
        }

        [HttpDelete("history/clear")]
        public ActionResult ClearHistory()
        {
            _service.ClearHistory();
            return NoContent();
        }
    }
}
