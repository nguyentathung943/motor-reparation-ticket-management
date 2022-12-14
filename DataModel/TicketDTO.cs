using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Common;
namespace DataModel;

public class TicketDTO
{
    public int? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string TicketStatus { get; set; }
    public string CreatedByUser { get; set; }
    public int? UserId { get; set; }
}

