using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneAdvanced.CarePlan.WebAPIs.Application;
using OneAdvanced.CarePlan.WebAPIs.Application.Models;
using OneAdvanced.CarePlan.WebAPIs.Application.Utils;
using OneAdvanced.CarePlan.WebAPIs.Application.Utils.Hypermedia;

namespace OneAdvanced.CarePlan.WebAPIs.Controllers
{
    [ApiController]
    [ApiKey]
    [Route("api/care-plans")]
    public class CarePlanController : Controller
    {
        private readonly CarePlanService _service;

        public CarePlanController(CarePlanService service)
        {
            _service = service;
        }


        /// <response code="201">Return the newly added care plan record.</response>
        /// <response code="400">Return the errors for the given care plan record.</response>
        /// <response code="403">Return the unauthorized errors when api key header is not provide or not valid.</response>
        /// <response code="500">Return internal server error if something has gone wrong.</response>
        /// <remarks> Add a new care plan record to the application.</remarks>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetCarePlanModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsDescription))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorsDescription))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorDescription))]
        public IActionResult CreateNew([FromBody] CreateCarePlanModel model)
        {
            var (isValid, errorsDescription) = model.Validate();
            if (isValid)
            {
                var result = _service.Create(model);
                if (result != null)
                {
                    var returnResult = result.Completed ? (GetCarePlanModel)result : (GetUncompletedCarePlanModel)result;
                    returnResult.Apply(GetLink(returnResult.CarePlanId), UpdateLink(returnResult.CarePlanId), DeleteLink(returnResult.CarePlanId));

                    return StatusCode(StatusCodes.Status201Created, returnResult);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerErrorDescription());
            }

            return StatusCode(StatusCodes.Status400BadRequest, errorsDescription);
        }

        /// <response code="200">Return the care plan record for given care_plan_id.</response>
        /// <response code="403">Return the unauthorized errors when api key header is not provide or not valid.</response>
        /// <response code="404">For the given care_plan_id is not found any record.</response>
        /// <remarks> Get care plan information for given care_plan_id.</remarks>
        [Route("{care_plan_id}")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCarePlanModel))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorsDescription))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([FromRoute(Name = "care_plan_id")] int carePlanId)
        {
            var result = _service.GetById(carePlanId);
            if (result != null)
            {
                var returnResult = result.Completed ? (GetCarePlanModel)result : (GetUncompletedCarePlanModel)result;
                returnResult.Apply(UpdateLink(returnResult.CarePlanId), DeleteLink(returnResult.CarePlanId));
                return StatusCode(StatusCodes.Status200OK, returnResult);

            }
            return NotFound(NoContent);
        }

        /// <response code="200">Return information for all care plans.</response>
        /// <response code="403">Return the unauthorized errors when api key header is not provide or not valid.</response>
        /// <remarks> Get information for all care plans.</remarks>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCarePlanModel[]))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorsDescription))]
        public IActionResult GetAll([FromQuery(Name = "patient_name")] string patientName)
        {
            var result = _service.GetAll(patientName);
            if (result != null)
            {
                var returnResult = result
                    .Select(x => x.Completed ? (GetCarePlanModel)x : (GetUncompletedCarePlanModel)x)
                    .ToList();
                returnResult.ForEach(x => x.Apply(GetLink(x.CarePlanId), UpdateLink(x.CarePlanId), DeleteLink(x.CarePlanId)));

                return StatusCode(StatusCodes.Status200OK, returnResult.ToArray());
            }
            return StatusCode(StatusCodes.Status200OK, Array.Empty<GetUncompletedCarePlanModel>());
        }

        /// <response code="204">Update has done successfully.</response>
        /// <response code="400">Return the errors for the given care plan record.</response>
        /// <response code="403">Return the unauthorized errors when api key header is not provide or not valid.</response>
        /// <response code="404">For the given care_plan_id is not found any record.</response>
        /// <response code="500">Return internal server error if something has gone wrong.</response>
        /// <remarks> Update care plan(totally replace an existing record).</remarks>
        [Route("{care_plan_id}")]
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsDescription))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorsDescription))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorDescription))]
        public IActionResult Update([FromRoute(Name = "care_plan_id")] int carePlanId, [FromBody] UpdateCarePlanModel model)
        {
            var oldEntity = _service.GetById(carePlanId);
            if (oldEntity != null)
            {
                var (isValid, errorsDescription) = model.Validate();
                if (isValid)
                {
                    var result = _service.Update(carePlanId, model);
                    if (result)
                        return StatusCode(StatusCodes.Status204NoContent);
                    return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerErrorDescription());
                }
                return StatusCode(StatusCodes.Status400BadRequest, errorsDescription);
            }
            return NotFound(NoContent);
        }


        /// <response code="204">Delete has done successfully.</response>
        /// <response code="403">Return the unauthorized errors when api key header is not provide or not valid.</response>
        /// <response code="404">For the given care_plan_id is not found any record.</response>
        /// <response code="500">Return internal server error if something has gone wrong.</response>
        /// <remarks> Delete care plan record from the application.</remarks>
        [Route("{care_plan_id}")]
        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorsDescription))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorDescription))]
        public IActionResult Delete([FromRoute(Name = "care_plan_id")] int carePlanId)
        {
            var oldEntity = _service.GetById(carePlanId);
            if (oldEntity != null)
            {
                var result = _service.Delete(carePlanId);
                if (result)
                    return StatusCode(StatusCodes.Status204NoContent, NoContent);
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerErrorDescription());
            }
            return NotFound(NoContent);
        }

        private new object NoContent => new { };
        private HyperLink GetLink(int carePlanId) => new($"/api/care-plans/{carePlanId}", "self", "GET");
        private HyperLink UpdateLink(int carePlanId) => new($"/api/care-plans/{carePlanId}", "update_care", "UPDATE");
        private HyperLink DeleteLink(int carePlanId) => new($"/api/care-plans/{carePlanId}", "delete_care", "DELETE");
    }
}
