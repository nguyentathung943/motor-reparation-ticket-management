using DataModel;
using MotorReparationTicketWASM.Service.IService;
using System.Text;
using System.Text.Json;

namespace MotorReparationTicketWASM.Service
{
    public class TicketHttpService: ITicketHttpService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public TicketHttpService(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<TicketDTO>> GetAllTickets()
        {
            var response = await _client.GetAsync("api/tickets");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var ticketList = JsonSerializer.Deserialize<List<TicketDTO>>(content, _options);
            return ticketList;
        }

        public async Task<TicketDTO> GetTicketById(int ticketId)
        {
            var response = await _client.GetAsync($"api/tickets/{ticketId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var ticket = JsonSerializer.Deserialize<TicketDTO>(content, _options);
            return ticket;
        }

        private StringContent ConvertDTOToBodyStringContent<T>(T model)
        {
            var json = JsonSerializer.Serialize<T>(model);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            return stringContent;
        }

        public async Task<bool> CreateTicket(TicketCreateUpdateDTO ticketModel)
        {
            ticketModel.UserId = 2;

            var stringBodyContent = ConvertDTOToBodyStringContent(ticketModel);

            var response = await _client.PostAsync("api/tickets", stringBodyContent);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var isTicketCreated = JsonSerializer.Deserialize<int>(content, _options);
            return isTicketCreated == 1;
        }

        public async Task<bool> UpdateTicket(int ticketId, TicketCreateUpdateDTO ticketModel)
        {
            var stringBodyContent = ConvertDTOToBodyStringContent(ticketModel);

            var response = await _client.PutAsync($"api/tickets/{ticketId}", stringBodyContent);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var isTicketUpdated = JsonSerializer.Deserialize<int>(content, _options);
            return isTicketUpdated == 1;
        }

        public async Task<bool> DeleteTicket(int ticketId)
        {
            var response = await _client.DeleteAsync($"api/tickets/{ticketId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var isTicketDeleted = JsonSerializer.Deserialize<int>(content, _options);
            return isTicketDeleted == 1;
        }
    }
}
