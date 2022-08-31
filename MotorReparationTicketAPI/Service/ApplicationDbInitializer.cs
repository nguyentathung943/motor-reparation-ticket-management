using Common;
using DataAccess.DataContext;
using DataAccess.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MotorReparationTicketAPI.Service;

public class ApplicationDbInitializer
{
    public static void Initialize(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            var _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
            var _userManager =  serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            
            context.Database.EnsureCreated();
            try
            {
                if (context.Database.GetPendingMigrations().Count() > 0)
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            
            if (context.Roles.Any(x => x.Name == Role.USER)) return;
            
            _roleManager.CreateAsync(new ApplicationRole()
            {
                Name = Role.ADMIN
            }).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new ApplicationRole()
            {
                Name = Role.USER
            }).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            }, "Admin123*").GetAwaiter().GetResult();
            
            _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = "user@gmail.com",
                Email = "user@gmail.com",
                EmailConfirmed = true
            }, "Admin123*").GetAwaiter().GetResult();
            
            
            ApplicationUser adminUser = context.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(adminUser, Role.ADMIN).GetAwaiter().GetResult();
            
            ApplicationUser normalUser = context.Users.FirstOrDefault(u => u.Email == "user@gmail.com");
            _userManager.AddToRoleAsync(normalUser, Role.USER).GetAwaiter().GetResult();

            if (!context.Tickets.Any())
            {
                context.Tickets.AddRange(
                    new List<Ticket>()
                    {
                        new Ticket()
                        {
                            CreatedAt = DateTime.Now,
                            Description = "Ticket 1 Description",
                            TicketStatus = TicketStatus.Todo,
                            Title = "Ticket 1 Title",
                            UserId =1
                        },
                        new Ticket()
                        {
                            CreatedAt = DateTime.Now,
                            Description = "Ticket 2 Description",
                            TicketStatus = TicketStatus.InProgress,
                            Title = "Ticket 2 Title",
                            UserId = 1
                        },
                        new Ticket()
                        {
                            CreatedAt = DateTime.Now,
                            Description = "Ticket 3 Description",
                            TicketStatus = TicketStatus.WorkDone,
                            Title = "Ticket 3 Title",
                            UserId = 2
                        },
                        new Ticket()
                        {
                            CreatedAt = DateTime.Now,
                            Description = "Ticket 4 Description",
                            TicketStatus = TicketStatus.Todo,
                            Title = "Ticket 4 Title",
                            UserId = 2
                        }
                    }
                );
                context.SaveChanges();
                context.TicketWorkItems.AddRange(
                    new List<WorkItem>()
                    {
                        new WorkItem()
                        {
                            CreatedAt = DateTime.Now,
                            CreatedBy = 1,
                            Quantity = 10,
                            TicketId = 1,
                            UnitPrice = 100,
                            WorkItemType = WorkItemType.Labor,
                        },
                        new WorkItem()
                        {
                            CreatedAt = DateTime.Now,
                            CreatedBy = 1,
                            Quantity = 20,
                            TicketId = 2,
                            UnitPrice = 200,
                            WorkItemType = WorkItemType.Labor
                        },
                        new WorkItem()
                        {
                            CreatedAt = DateTime.Now,
                            CreatedBy = 2,
                            Quantity = 30,
                            TicketId = 3,
                            UnitPrice = 300,
                            WorkItemType = WorkItemType.Part
                        },
                        new WorkItem()
                        {
                            CreatedAt = DateTime.Now,
                            CreatedBy = 2,
                            Quantity = 40,
                            TicketId = 4,
                            UnitPrice = 400,
                            WorkItemType = WorkItemType.Part
                        }
                    }    
                );
            }
            context.SaveChanges();
        }
    }
}