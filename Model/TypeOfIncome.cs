using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class TypeOfIncome
    {
        [Key]
        public int Id { get; set; }
        public string? Type { get; set; }
        public InAndOutComeEnum InAndOutCome { get; set; }
    }
}
