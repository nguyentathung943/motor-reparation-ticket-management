using DataModel;

namespace MotorReparationTicketAPI.Repository.IRepository;

public interface ITicketRepository
{
    public Task<IEnumerable<TicketDTO>> GetAllTickets();
    public Task<int> CreateTicket(TicketCreateUpdateDTO ticketModel);
    public Task<TicketDTO> GetTicketById(int ticketId);
    public Task<int> UpdateTicket(int ticketId, TicketCreateUpdateDTO ticketModel);
    public Task<int> DeleteTicket(int ticketId);
}
