
namespace Models.Mapping.Dto
{
    public class OperationAddDto
    {
        const decimal Factor = 100;
        private int income;

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
