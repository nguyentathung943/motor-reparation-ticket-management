using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common;

namespace DataAccess.Entity;

public class WorkItem
{
    [Key]
    public int ID { get; set; }
    
    public int TicketId { get; set; }
    [ForeignKey("TicketId")]
    public Ticket Ticket { get; set; }
    
    [Required]
    public WorkItemType WorkItemType { get; set; }
    
    [Required]
    public double UnitPrice { get; set; }
    
    [Required]
    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public int CreatedBy { get; set; }
}