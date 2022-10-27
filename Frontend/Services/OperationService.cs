using Frontend.ViewModel;

namespace Frontend.Services
{
    public class OperationService
    {
        private readonly HttpClient _httpClient;

        public OperationService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<List<OperationViewModel>> GetListOperation(DateTime date)
        {
            return await _httpClient.GetFromJsonAsync<List<OperationViewModel>>(GetFinalUrl($"GetListOperation/list?date={date.ToString("yyyy.MM.dd")}"));
        }

        public async Task AddOperation(OperationViewModel operation)
        {
            await _httpClient.PostAsJsonAsync(GetFinalUrl("AddOperation"), operation);
        }

        public async Task UpdateOperation(OperationViewModel operation)
        {
            await _httpClient.PutAsJsonAsync(GetFinalUrl("UpdateOperation"), operation);
        }

        public async Task DeleteOperation(int operationId)
        {
            await _httpClient.DeleteAsync(GetFinalUrl("DeleteOperation/") + operationId);
        }

        private string GetFinalUrl(string request)
        {
            return _httpClient.BaseAddress + "Operation/" + request;
        }
    }
}
