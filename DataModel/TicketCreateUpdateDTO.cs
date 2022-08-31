using System.ComponentModel.DataAnnotations;

namespace DataModel;

public class TicketCreateUpdateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string TicketStatus { get; set; }
    public int UserId { get; set; }
}
