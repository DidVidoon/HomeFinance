using System.ComponentModel.DataAnnotations;

namespace Models.Mapping
{
    public class TypeOfIncomeViewModel
    {
        [Key]
        public int Id { get; set; }
        public string? Type { get; set; }
        public InAndOutComeEnum InAndOutCome { get; set; }
    }
}
