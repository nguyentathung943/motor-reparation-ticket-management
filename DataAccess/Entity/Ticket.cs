using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common;

namespace DataAccess.Entity
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public int UserId { get; set; }

        [Required]
        public string TicketStatus { get; set; }
        
        public virtual ICollection<WorkItem> WorkItems { get; set; }
    }
}