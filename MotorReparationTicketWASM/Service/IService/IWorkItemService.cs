using DataModel;

namespace MotorReparationTicketWASM.Service.IService
{
	public interface IWorkItemHttpService
	{
        Task<List<TicketWorkItemDTO>> GetAllWorkItemByTicketId(int ticketId);
        Task<TicketWorkItemDTO> GetTicketWorkItemById(int itemId);
        Task<bool> CreateTicketWorkItem(TicketWorkItemCreateUpdateDTO itemModel);
        Task<bool> UpdateTicketWorkItem(int itemId, TicketWorkItemCreateUpdateDTO itemModel);
        Task<bool> DeleteTicketWorkItem(int itemId);
    }
}
