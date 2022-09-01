using System.ComponentModel.DataAnnotations;

namespace DataModel;

public class TicketWorkItemCreateUpdateDTO
{
    public int TicketId { get; set; }
    public string WorkItemType { get; set; }
    [Required(ErrorMessage = "Please enter Unit Price/Labor Rate")]
    public double UnitPrice { get; set; }
    [Required(ErrorMessage = "Please enter Quantity/Hours")]
    public int Quantity { get; set; }
    public int UserId { get; set; }
}
