namespace DataModel;

public class TicketWorkItemCreateUpdateDTO
{
    public int TicketId { get; set; }
    public string WorkItemType { get; set; }
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
    public int UserId { get; set; }
}