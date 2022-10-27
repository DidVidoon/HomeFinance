using Models;
using Models.Mapping;
using Models.Mapping.Dto;

namespace Services.IRepository
{
    public interface ITypeService
    {
        public Task<List<TypeOfIncomeViewModel>> GetAllAsync();
        public Task<List<TypeOfIncomeViewModel>> GetAllAsync(InAndOutComeEnum inAndOutCome);
        public Task<bool> AddAsync(TypeOfIncomeAddDto typeOfIncomeAddDto);
        public Task<bool> UpdateTypeAsync(TypeOfIncomeEditDto typeOfIncomeEditDto);
        public Task<DeleteRequestResultEnum> DeleteTypeAsync(int typeId);
    }
}
