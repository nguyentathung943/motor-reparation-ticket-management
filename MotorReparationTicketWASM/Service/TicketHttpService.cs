using DataModel;
using MotorReparationTicketWASM.Service.IService;
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
            var products = JsonSerializer.Deserialize<List<TicketDTO>>(content, _options);
            return products;
        }

        public async Task<TicketDTO> GetTicketById(int ticketId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateTicket(TicketCreateUpdateDTO ticketModel)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateTicket(int ticketId, TicketCreateUpdateDTO ticketModel)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteTicket(int ticketId)
        {
            throw new NotImplementedException();
        }
    }
}
