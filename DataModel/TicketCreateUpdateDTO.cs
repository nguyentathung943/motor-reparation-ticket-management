using System.ComponentModel.DataAnnotations;

namespace DataModel;

public class TicketCreateUpdateDTO
{
    [Required(ErrorMessage = "Please enter ticket title")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Please enter ticket description")]
    public string Description { get; set; }
    public string TicketStatus { get; set; }
    public int UserId { get; set; }
}
