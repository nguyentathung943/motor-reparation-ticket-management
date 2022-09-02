using Blazored.LocalStorage;
using Common;
using DataModel;
using DataModel.Authentication;
using MotorReparationTicketWASM.Service.IService;
using System.Text;
using System.Text.Json;

namespace MotorReparationTicketWASM.Service
{
	public class TicketWorkItemHttpService : IWorkItemHttpService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private readonly ILocalStorageService _localStorageService;


        public TicketWorkItemHttpService(HttpClient client, ILocalStorageService localStorageService)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _localStorageService = localStorageService;
        }

        private StringContent ConvertDTOToBodyStringContent<T>(T model)
        {
            var json = JsonSerializer.Serialize<T>(model);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            return stringContent;
        }

        public async Task<List<TicketWorkItemDTO>> GetAllWorkItemByTicketId(int ticketId)
        {
            var response = await _client.GetAsync($"api/ticket_items/get-work-items-by-ticket-id/{ticketId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var workItemList = JsonSerializer.Deserialize<List<TicketWorkItemDTO>>(content, _options);
            return workItemList;
        }

        public async Task<TicketWorkItemDTO> GetTicketWorkItemById(int itemId)
        {
            var response = await _client.GetAsync($"api/ticket_items/{itemId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var item = JsonSerializer.Deserialize<TicketWorkItemDTO>(content, _options);
            return item;
        }

        public async Task<bool> CreateTicketWorkItem(TicketWorkItemCreateUpdateDTO itemModel)
		{
            var userInfo = await _localStorageService.GetItemAsync<UserInfoDTO>(Auth.USER_DETAIL);
            itemModel.UserId = userInfo.Id;

            var stringBodyContent = ConvertDTOToBodyStringContent(itemModel);

            var response = await _client.PostAsync("api/ticket_items", stringBodyContent);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var isItemCreated = JsonSerializer.Deserialize<int>(content, _options);
            return isItemCreated == 1;
        }


        public async Task<bool> UpdateTicketWorkItem(int itemId, TicketWorkItemCreateUpdateDTO itemModel)
		{
            var stringBodyContent = ConvertDTOToBodyStringContent(itemModel);

            var response = await _client.PutAsync($"api/ticket_items/{itemId}", stringBodyContent);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var isItemUpdated = JsonSerializer.Deserialize<int>(content, _options);
            return isItemUpdated == 1;
        }

        public async Task<bool> DeleteTicketWorkItem(int itemId)
        {
            var response = await _client.DeleteAsync($"api/ticket_items/{itemId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var isItemDeleted = JsonSerializer.Deserialize<int>(content, _options);
            return isItemDeleted == 1;
        }
    }
}
