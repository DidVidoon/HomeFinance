using Models.Mapping;
using Models.Mapping.Dto;

namespace Services.IRepository
{
    public interface IOperationService
    {
        public Task<List<OperationViewModel>> GetAllAsync(DateTime date);
        public Task<List<OperationViewModel>> GetAllAsync(DateTime firstDate, DateTime secondDate);
        public Task<bool> AddAsync(OperationAddDto operationAddDto);
        public Task<TransactionResultViewModel> GetDailyBalance(DateTime date);
        public Task<TransactionResultViewModel> GetMonthlyBalance(DateTime firstDate, DateTime secondDate);
        public Task<bool> UpdateOperationAsync(OperationEditDto operationEditDto);
        public Task<bool> DeleteOperationAsync(int operationId);
    }
}
