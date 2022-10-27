using Microsoft.AspNetCore.Mvc;
using Services.IRepository;

namespace Task_12.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IOperationService _operationService;

        public BalanceController(IOperationService acessToOperation)
        {
            _operationService = acessToOperation;
        }

        [HttpGet("daily")]
        public async Task<IActionResult> GetDailyBalance([FromQuery(Name = "date")] DateTime date)
        {
            var transactionResult = await _operationService.GetDailyBalance(date);

            return Ok(transactionResult);
        }

        [HttpGet("period")]
        public async Task<IActionResult> GetMonthlyBalance([FromQuery(Name = "fromDate")] DateTime fromDate, [FromQuery(Name = "toDate")] DateTime toDate)
        {
            var transactionResult = await _operationService.GetMonthlyBalance(fromDate, toDate);

            return Ok(transactionResult);
        }
    }
}
