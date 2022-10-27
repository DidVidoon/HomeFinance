
namespace Models.Mapping
{
    public class TransactionResultViewModel
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalConsumption { get; set; }
        public decimal Balance => TotalIncome + TotalConsumption;

        public List<Operation>? ListOfOperations { get; set; }
    }
}
