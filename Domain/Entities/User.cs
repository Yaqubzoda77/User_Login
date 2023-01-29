namespace Domain.Entities;

public class User
{
    public int  UserId{ get; set; }
    public string FirstName { get; set; }
    public string  email{ get; set; }
    public string NumberPhone{ get; set; }
    public int  Password{ get; set; }
    public List<UserRole> UserRole { get; set; }
    public List<UserLogin> UserLogin { get; set; }
}