using DataModel;

namespace MotorReparationTicketAPI.Repository.IRepository;

public interface IWorkItemRepository
{
    public Task<IEnumerable<TicketWorkItemDTO>> GetAllWorkItemsByTicketId(int itemId);
    public Task<int> CreateWorkItem(TicketWorkItemCreateUpdateDTO itemModel);
    public Task<TicketWorkItemDTO> GetWorkItemById(int itemId);
    public Task<int> UpdateWorkItem(int itemId, TicketWorkItemCreateUpdateDTO itemModel);
    public Task<int> DeleteWorkItem(int itemId);
}
