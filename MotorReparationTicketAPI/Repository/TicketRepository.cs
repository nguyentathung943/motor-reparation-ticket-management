using DataAccess.DataContext;
using DataAccess.Entity;
using DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotorReparationTicketAPI.Repository.IRepository;

namespace MotorReparationTicketAPI.Repository;

public class TicketRepository: ITicketRepository
{
    private readonly ApplicationDbContext _db;

    public TicketRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<IEnumerable<TicketDTO>> GetAllTickets()
    {
        var result =
            (
                from ticket in _db.Tickets
                join user in _db.Users 
                on ticket.UserId equals user.Id
                select new TicketDTO()
                {
                    Id = ticket.Id,
                    CreatedAt = ticket.CreatedAt,
                    Description = ticket.Description,
                    TicketStatus = ticket.TicketStatus,
                    Title = ticket.Title,
                    CreatedByUser = user.UserName,
                    UserId = user.Id
                }
            ).ToList();

        return result;
    }

    public async Task<int> CreateTicket(TicketCreateUpdateDTO ticketModel)
    {
        var ticket = new Ticket()
        {
            Description = ticketModel.Description,
            TicketStatus = ticketModel.TicketStatus,
            Title = ticketModel.Title,
            UserId = ticketModel.UserId
        };

        await _db.Tickets.AddAsync(ticket);
        return await _db.SaveChangesAsync();
    }

    public async Task<TicketDTO> GetTicketById(int ticketId)
    {
        var result =
        (
            from ticket in _db.Tickets
            join user in _db.Users
            on ticket.UserId equals user.Id
            where ticket.Id == ticketId
            select new TicketDTO()
            {
                Id = ticket.Id,
                CreatedAt = ticket.CreatedAt,
                Description = ticket.Description,
                TicketStatus = ticket.TicketStatus,
                Title = ticket.Title,
                CreatedByUser = user.UserName
            }
        ).FirstOrDefault();

        if (result == null)
        {
            throw new Exception($"No ticket was found with ID: {ticketId}");
        }

        return result;
    }

    public async Task<int> UpdateTicket(int ticketId, TicketCreateUpdateDTO ticketModel)
    {
        var ticket = await _db.Tickets.FindAsync(ticketId);

        if (ticket == null)
        {
            throw new Exception($"No ticket was found with ID: {ticketId}");
        }
        else 
        {
            ticket.Description = ticketModel.Description;
            ticket.TicketStatus = ticketModel.TicketStatus;
            ticket.Title = ticketModel.Title;
            _db.Tickets.Update(ticket);
        }
        
        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteTicket(int ticketId)
    {
        var ticket = await _db.Tickets.FindAsync(ticketId);
        if (ticket == null)
        {
            throw new Exception($"No ticket was found with ID: {ticketId}");
        }
        
        _db.Tickets.Remove(ticket);
        return await _db.SaveChangesAsync();
    }
}