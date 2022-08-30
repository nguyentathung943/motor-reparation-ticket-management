using System.ComponentModel.DataAnnotations;
using Common;

namespace DataAccess.Entity
{
    public class Ticket
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public int UserId { get; set; }

        [Required]
        public TicketStatus TicketStatus { get; set; }
        
        public virtual ICollection<WorkItem> WorkItems { get; set; }
    }
}