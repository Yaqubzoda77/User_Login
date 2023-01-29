namespace Domain.Dtos;

public class UserLoginDto
{
    public int  UserId{ get; set; }
    public string FirstName { get; set; }
    public int  email{ get; set; }
    public string NumberPhone{ get; set; }
    public int  Password{ get; set; }
}