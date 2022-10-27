using AutoMapper;
using Models.Mapping.Dto;

namespace Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TypeOfIncome, TypeOfIncomeViewModel>().ReverseMap();
            CreateMap<TypeOfIncome, TypeOfIncomeAddDto>().ReverseMap();
            CreateMap<TypeOfIncome, TypeOfIncomeEditDto>().ReverseMap();

            CreateMap<TransactionResult, TransactionResultViewModel>().ReverseMap();

            CreateMap<Operation, OperationViewModel>().ReverseMap();
            CreateMap<Operation, OperationAddDto>().ReverseMap();
            CreateMap<Operation, OperationEditDto>().ReverseMap();

        }
    }
}
