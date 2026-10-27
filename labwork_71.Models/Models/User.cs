using Microsoft.AspNetCore.Identity;

namespace labwork_71.Models;

public class User : IdentityUser<int>
{
    public string AvatarUrl { get; set; }
    public bool IsAdmin { get; set; } =  false;
}