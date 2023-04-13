using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NTL_Book.Models;

// Add profile data for application users by adding properties to the NTL_BookUser class
public class ApplicationUser : IdentityUser
{
    [StringLength(50)]
    public string FullName { get; set; } =null!;

    [StringLength(100)]
    public string HomeAddress { get; set; }=null!;
    [StringLength(100)]
    public override string? PhoneNumber { get; set; }=null!;
}

