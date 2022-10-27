using System.ComponentModel.DataAnnotations;

namespace Models.Mapping.Dto
{
    public class OperationEditDto
    {
        const decimal Factor = 100;
        private int income;

        [Key]
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string? GoalOfOperation { get; set; }
        public DateTime Date { get; set; }

        public decimal Income
        {
            get { return income / Factor; }
            set { income = (int)(value * Factor); }
        }
    }
}
