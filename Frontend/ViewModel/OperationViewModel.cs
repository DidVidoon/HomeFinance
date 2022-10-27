using System.ComponentModel.DataAnnotations;

namespace Frontend.ViewModel
{
    public class OperationViewModel
    {
        const decimal Factor = 100;
        private int income;
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string? GoalOfOperation { get; set; }
        public DateTime Date { get; set; }

        public decimal Income
        {
            get { return income / Factor; }
            set { income = (int)(value*Factor); }
        }
    }
}
