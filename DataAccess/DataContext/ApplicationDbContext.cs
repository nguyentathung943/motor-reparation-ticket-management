using DataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataContext;

public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<WorkItem> TicketWorkItems { get; set; }
}
