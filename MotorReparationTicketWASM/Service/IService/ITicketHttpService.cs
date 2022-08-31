using DataModel;

namespace MotorReparationTicketWASM.Service.IService
{
    public interface ITicketHttpService
    {
        Task<List<TicketDTO>> GetAllTickets();
        Task<TicketDTO> GetTicketById(int ticketId);
        Task<int> CreateTicket(TicketCreateUpdateDTO ticketModel);
        Task<int> UpdateTicket(int ticketId, TicketCreateUpdateDTO ticketModel);
        Task<int> DeleteTicket(int ticketId);
    }
}
