using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Mapping.Dto;
using Services;
using Services.IRepository;

namespace Task_12.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;

        public TypeController(ITypeService acessToTypes)
        {
            _typeService = acessToTypes;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetListTypes()
        {
            var listOfTypes = await _typeService.GetAllAsync();

            return Ok(listOfTypes);
        }

        [HttpGet("list-income")]
        public async Task<IActionResult> GetListTypesOfIncome()
        {
            var listOfTypes = await _typeService.GetAllAsync(InAndOutComeEnum.INCOME);

            return Ok(listOfTypes);
        }

        [HttpGet("list-outcome")]
        public async Task<IActionResult> GetListTypesOfOutcome()
        {
            var listOfTypes = await _typeService.GetAllAsync(InAndOutComeEnum.OUTCOME);

            return Ok(listOfTypes);
        }

        [HttpPost]
        public async Task<IActionResult> AddType([FromBody] TypeOfIncomeAddDto typeOfIncomeAddDto)
        {
            await _typeService.AddAsync(typeOfIncomeAddDto);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateType([FromBody] TypeOfIncomeEditDto typeOfIncomeAddEditDto)
        {
            var isSucessfullyUpdated = await _typeService.UpdateTypeAsync(typeOfIncomeAddEditDto);

            if (isSucessfullyUpdated)
                return Ok();

            return NotFound();
        }

        [HttpDelete("{typeId}")]
        public async Task<IActionResult> DeleteType([FromRoute] int typeId)
        {
            var deleteRequestResult = await _typeService.DeleteTypeAsync(typeId);

            if(deleteRequestResult == DeleteRequestResultEnum.SUCCESSFULLY)
                return NoContent();
            else if(deleteRequestResult == DeleteRequestResultEnum.NOT_FOUND)
                return NotFound();

            return BadRequest();
        }
    }
}
