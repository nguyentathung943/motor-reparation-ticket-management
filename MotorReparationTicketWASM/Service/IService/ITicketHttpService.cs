using DataModel;

namespace MotorReparationTicketWASM.Service.IService
{
    public interface ITicketHttpService
    {
        Task<List<TicketDTO>> GetAllTickets();
        Task<TicketDTO> GetTicketById(int ticketId);
        Task<bool> CreateTicket(TicketCreateUpdateDTO ticketModel);
        Task<bool> UpdateTicket(int ticketId, TicketCreateUpdateDTO ticketModel);
        Task<bool> DeleteTicket(int ticketId);
    }
}
