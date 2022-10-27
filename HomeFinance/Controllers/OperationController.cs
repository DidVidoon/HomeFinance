using Microsoft.AspNetCore.Mvc;
using Models.Mapping.Dto;
using Services.IRepository;
namespace Task_12.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IOperationService _operationService;

        public OperationController(IOperationService acessToOperationService)
        {
            _operationService = acessToOperationService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetListOperation([FromQuery(Name = "date")] DateTime date)
        {
            var listOfOperation = await _operationService.GetAllAsync(date.AddMonths(-1), date);

            return Ok(listOfOperation);
        }

        [HttpPost]
        public async Task<IActionResult> AddOperation([FromBody]OperationAddDto operationAddDto)
        {
            await _operationService.AddAsync(operationAddDto);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOperation([FromBody]OperationEditDto operationEditDto)
        {

            var isSuccessfullyUpdated = await _operationService.UpdateOperationAsync(operationEditDto);

            if (!isSuccessfullyUpdated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{operationId}")]
        public async Task<IActionResult> DeleteOperation([FromRoute] int operationId)
        {
            var isSuccesfullyDeleted =  await _operationService.DeleteOperationAsync(operationId);
            
            if (!isSuccesfullyDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
