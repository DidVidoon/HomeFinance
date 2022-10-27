using Frontend.ViewModel;

namespace Frontend.Services
{
    public class TypeService
    {
        private readonly HttpClient _httpClient;

        public TypeService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<List<TypeOfIncomeViewModel>> GetListType()
        {
            return await _httpClient.GetFromJsonAsync<List<TypeOfIncomeViewModel>>(GetFinalUrl("GetListTypes/list"));
        }

        public async Task<List<TypeOfIncomeViewModel>> GetListTypesOfIncome()
        {
            return await _httpClient.GetFromJsonAsync<List<TypeOfIncomeViewModel>>(GetFinalUrl("GetListTypesOfIncome/list-income"));
        }

        public async Task<List<TypeOfIncomeViewModel>> GetListTypesOfOutcome()
        {
            return await _httpClient.GetFromJsonAsync<List<TypeOfIncomeViewModel>>(GetFinalUrl("GetListTypesOfOutcome/list-outcome"));
        }

        public async Task AddType(TypeOfIncomeViewModel typeOfIncome)
        {
            await _httpClient.PostAsJsonAsync(GetFinalUrl("AddType"), typeOfIncome);
        }

        public async Task UpdateType(TypeOfIncomeViewModel typeOfIncome)
        {
            await _httpClient.PutAsJsonAsync(GetFinalUrl("UpdateType"), typeOfIncome);
        }

        public async Task DeleteTypeAsync(int typeId)
        {
            await _httpClient.DeleteAsync(GetFinalUrl("DeleteType/") + typeId);
        }

        private string GetFinalUrl(string request)
        {
            return _httpClient.BaseAddress + "Type/" + request;
        }
    }
}
