using System.ComponentModel.DataAnnotations;

namespace Models.Mapping.Dto
{
    public class TypeOfIncomeEditDto
    {
        [Key]
        public int Id { get; set; }
        public string? Type { get; set; }
        public InAndOutComeEnum InAndOutCome { get; set; }
    }
}
