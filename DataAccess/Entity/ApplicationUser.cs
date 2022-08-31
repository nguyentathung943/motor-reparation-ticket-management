using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entity;
public class ApplicationUser: IdentityUser<int>
{
    
}
