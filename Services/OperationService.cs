using Models;
using Services.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.Mapping;
using AutoMapper;
using Models.Mapping.Dto;
using Infrastructure;

namespace Services
{
    public class OperationService : IOperationService
    {
        private readonly OperationContext _context;
        private readonly IMapper _mapper;

        public OperationService(OperationContext operationContext, IMapper mapper)
        {
            _context = operationContext;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(OperationAddDto operationAddDto)
        {
            await _context.Operations.AddAsync(_mapper.Map<Operation>(operationAddDto));
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<OperationViewModel>> GetAllAsync(DateTime date)
        {
            var operationsList = await _context.Operations.Where(u => u.Date.Date == date.Date).ToListAsync();

            return _mapper.Map<List<OperationViewModel>>(operationsList);
        }

        public async Task<List<OperationViewModel>> GetAllAsync(DateTime fromDate, DateTime toDate)
        {
            var operationsList = await _context.Operations.Where(u => (u.Date.Date >= fromDate.Date) && (u.Date.Date <= toDate.Date)).ToListAsync();

            return _mapper.Map<List<OperationViewModel>>(operationsList);
        }

        public async Task<TransactionResultViewModel> GetDailyBalance(DateTime date)
        {
            var listOfOperations = await GetAllAsync(date);

            IEnumerable<decimal> income =
                from operation in listOfOperations
                where operation.Date.Date == date && operation.Income >= 0
                select operation.Income;

            IEnumerable<decimal> consumption =
                from operation in listOfOperations
                where operation.Date.Date == date && operation.Income < 0
                select operation.Income;

            var transactionResultViewModel = new TransactionResultViewModel
            {
                ListOfOperations = _mapper.Map<List<Operation>>(listOfOperations),
                TotalConsumption = consumption.Sum(),
                TotalIncome = income.Sum()
            };

            return transactionResultViewModel;
        }

        public async Task<TransactionResultViewModel> GetMonthlyBalance(DateTime firstDate, DateTime secondDate)
        {
            var listOfOperations = await GetAllAsync(firstDate, secondDate);

            IEnumerable<decimal> income =
                from operation in listOfOperations
                where operation.Date.Date >= firstDate && operation.Date.Date <= secondDate && operation.Income >= 0
                select operation.Income;

            IEnumerable<decimal> consumption =
                from operation in listOfOperations
                where operation.Date.Date >= firstDate && operation.Date.Date <= secondDate && operation.Income < 0
                select operation.Income;

            var transactionResultViewModel = new TransactionResultViewModel
            {
                ListOfOperations = _mapper.Map<List<Operation>>(listOfOperations),
                TotalConsumption = consumption.Sum(),
                TotalIncome = income.Sum()
            };

            return transactionResultViewModel;
        }

        public async Task<bool> UpdateOperationAsync(OperationEditDto operationEditDto)
        {
            Operation currentOperation = await _context.Operations.FirstOrDefaultAsync(u => (u.Id == operationEditDto.Id));

            if (currentOperation == null) { return false; }

            currentOperation.Id = operationEditDto.Id;
            currentOperation.Date = operationEditDto.Date;
            currentOperation.GoalOfOperation = operationEditDto.GoalOfOperation;
            currentOperation.Income = operationEditDto.Income;

            _context.Operations.Update(currentOperation);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteOperationAsync(int operationId)
        {
            Operation? operation = await _context.Operations.FirstOrDefaultAsync(u => (u.Id == operationId));

            if (operation == null) { return false; }

            _context.Operations.Remove(operation);
            _context.SaveChanges();

            return true;
        }
    }
}
