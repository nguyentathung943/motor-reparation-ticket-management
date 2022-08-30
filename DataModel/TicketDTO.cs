using System.ComponentModel.DataAnnotations;
using Common;
namespace DataModel;

public class TicketDTO
{
    public int ID { get; set; }
    [Required(ErrorMessage = "Please enter ticket title")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Please enter ticket description")]
    public string Description { get; set; }
        
    public DateTime CreatedAt { get; set; }
        
    public int UserId { get; set; }
    
    public TicketStatus TicketStatus { get; set; }
}
