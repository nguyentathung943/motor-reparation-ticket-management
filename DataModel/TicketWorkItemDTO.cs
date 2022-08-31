using System.ComponentModel.DataAnnotations;
using Common;

namespace DataModel;

public class TicketWorkItemDTO
{
    public int? Id { get; set; }
    public int TicketId { get; set; }
    public string WorkItemType { get; set; }
    [Required(ErrorMessage = "Please enter Unit Price/Labor Rate")]
    public double UnitPrice { get; set; }
    [Required(ErrorMessage = "Please enter Quantity/Hours")]
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedByUser { get; set; }
    public int? UserId { get; set; }
}
