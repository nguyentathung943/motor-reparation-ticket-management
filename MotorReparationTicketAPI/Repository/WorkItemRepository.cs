using DataAccess.DataContext;
using DataAccess.Entity;
using DataModel;
using Microsoft.EntityFrameworkCore;
using MotorReparationTicketAPI.Repository.IRepository;

namespace MotorReparationTicketAPI.Repository;

public class WorkItemRepository: IWorkItemRepository
{
    private readonly ApplicationDbContext _db;

    public WorkItemRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<IEnumerable<TicketWorkItemDTO>> GetAllWorkItemsByTicketId(int ticketId)
    {
        var result =
        (
            from item in _db.TicketWorkItems
            join user in _db.Users 
            on item.CreatedBy equals user.Id
            where item.TicketId == ticketId
            select new TicketWorkItemDTO()
            {
                Id = item.Id,
                TicketId = item.TicketId,
                WorkItemType = item.WorkItemType,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                CreatedAt = item.CreatedAt,
                CreatedByUser = user.UserName,
                UserId = user.Id
            }
        ).ToList();

        return result;
    }

    public async Task<int> CreateWorkItem(TicketWorkItemCreateUpdateDTO itemModel)
    {
        var item = new WorkItem()
        {
            TicketId =  itemModel.TicketId,
            UnitPrice = itemModel.UnitPrice,
            Quantity = itemModel.Quantity,
            CreatedBy = itemModel.UserId,
            WorkItemType = itemModel.WorkItemType
        };

        _db.TicketWorkItems.Add(item);
        return await _db.SaveChangesAsync();
    }

    public async Task<TicketWorkItemDTO> GetWorkItemById(int itemId)
    {
        var result = await
        (
            from item in _db.TicketWorkItems
            join user in _db.Users
                on item.CreatedBy equals user.Id
            where item.Id == itemId
            select new TicketWorkItemDTO()
            {
                Id = item.Id,
                TicketId = item.TicketId,
                WorkItemType = item.WorkItemType,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                CreatedAt = item.CreatedAt,
                CreatedByUser = user.UserName,
                UserId = user.Id
            }
        ).FirstOrDefaultAsync();
        
        if (result == null)
        {
            throw new Exception($"No work item was found with ID: {itemId}");
        }

        return result;
    }

    public async Task<int> UpdateWorkItem(int itemId, TicketWorkItemCreateUpdateDTO itemModel)
    {
        var item = await _db.TicketWorkItems.FindAsync(itemId);
        
        if (item == null)
        {
            throw new Exception($"No work item was found with ID: {itemModel}");
        }
        else
        {
            item.UnitPrice = itemModel.UnitPrice;
            item.Quantity = itemModel.Quantity;
            item.WorkItemType = itemModel.WorkItemType;
            _db.TicketWorkItems.Update(item);
        }

        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteWorkItem(int itemId)
    {
        var item = await _db.TicketWorkItems.FindAsync(itemId);
        
        if (item == null)
        {
            throw new Exception($"No work item was found with ID: {itemId}");
        }

        _db.TicketWorkItems.Remove(item);
        return await _db.SaveChangesAsync();
    }
}
