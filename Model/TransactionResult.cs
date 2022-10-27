
namespace Models
{
    public class TransactionResult
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalConsumption { get; set; }
        public decimal Balance => TotalIncome + TotalConsumption;

        public List<Operation>? ListOfOperations { get; set; }
    }
}
