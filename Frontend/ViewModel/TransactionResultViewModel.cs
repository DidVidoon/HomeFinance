
namespace Frontend.ViewModel
{
    public class TransactionResultViewModel
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalConsumption { get; set; }
        public decimal Balance => TotalIncome + TotalConsumption;

        public List<OperationViewModel>? ListOfOperations { get; set; }
    }
}
