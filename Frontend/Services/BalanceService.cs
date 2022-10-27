using Frontend.ViewModel;

namespace Frontend.Services
{
    public class BalanceService
    {
        private readonly HttpClient _httpClient;

        public BalanceService(HttpClient client)
        {
            _httpClient = client;
        }
        
        public async Task<TransactionResultViewModel> GetDailyBalance(DateTime date)
        {
            return await _httpClient.GetFromJsonAsync<TransactionResultViewModel>(GetFinalUrl($"GetDailyBalance/daily?date={date.ToString("yyyy.MM.dd")}"));
        }

        public async Task<TransactionResultViewModel> GetMounthBalance(DateTime fromDate, DateTime toDate)
        {
            return await _httpClient.GetFromJsonAsync<TransactionResultViewModel>(GetFinalUrl($"GetMonthlyBalance/period?fromDate={fromDate.ToString("yyyy.MM.dd")}&toDate={toDate.ToString("yyyy.MM.dd")}"));
        }

        private string GetFinalUrl(string request)
        {
            return _httpClient.BaseAddress + "Balance/" + request;
        }
    }
}
