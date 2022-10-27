using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.IRepository;
using Models.Mapping;
using Models.Mapping.Dto;
using Infrastructure;

namespace Services
{
    public class TypeService : ITypeService
    {
        private readonly OperationContext _context;
        private readonly IMapper _mapper;

        public TypeService(OperationContext operationContext, IMapper mapper)
        {
            _context = operationContext;
            _mapper = mapper;
        }

        public async Task<List<TypeOfIncomeViewModel>> GetAllAsync()
        {
            var typeOfIncomeList = await _context.TypeOfIncomes.ToListAsync();

            return _mapper.Map<List<TypeOfIncomeViewModel>>(typeOfIncomeList);
        }

        public async Task<List<TypeOfIncomeViewModel>> GetAllAsync(InAndOutComeEnum inAndOutCome)
        {
            var typeOfIncomeList = await _context.TypeOfIncomes.Where(u => u.InAndOutCome == InAndOutComeEnum.INCOME).ToListAsync();

            return _mapper.Map<List<TypeOfIncomeViewModel>>(typeOfIncomeList);
        }

        public async Task<bool> AddAsync(TypeOfIncomeAddDto TypeOfIncomeAddDto)
        {
            await _context.TypeOfIncomes.AddAsync(_mapper.Map<TypeOfIncome>(TypeOfIncomeAddDto));
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateTypeAsync(TypeOfIncomeEditDto typeOfIncomeEditDto)
        {
            TypeOfIncome currentTypeOfImcome = await _context.TypeOfIncomes.FirstOrDefaultAsync(u => (u.Id == typeOfIncomeEditDto.Id));
            if (currentTypeOfImcome == null) 
                return false;

            currentTypeOfImcome.Id = typeOfIncomeEditDto.Id;
            currentTypeOfImcome.InAndOutCome = typeOfIncomeEditDto.InAndOutCome;
            currentTypeOfImcome.Type = typeOfIncomeEditDto.Type;

            _context.TypeOfIncomes.Update(currentTypeOfImcome);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<DeleteRequestResultEnum> DeleteTypeAsync(int typeId)
        {
            var deleteRequestResult = DeleteRequestResultEnum.SUCCESSFULLY;

            TypeOfIncome typeOfIncome = await _context.TypeOfIncomes.FirstOrDefaultAsync(u => (u.Id == typeId));

            if (typeOfIncome == null) 
            { 
                deleteRequestResult = DeleteRequestResultEnum.NOT_FOUND; 
                return deleteRequestResult;
            }

            if (await _context.Operations.FirstOrDefaultAsync(u => (u.TypeId == typeId)) != null)
            {
                deleteRequestResult = DeleteRequestResultEnum.CANNOT_BE_DELETED;
                return deleteRequestResult;
            }

            _context.TypeOfIncomes.Remove(typeOfIncome);
            await _context.SaveChangesAsync();

            return deleteRequestResult;
        }
    }
}
