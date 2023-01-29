namespace Domain.Entities;

public class UserLogin
{
    public int UserLoginId { get; set; }
    public DateTime  LoginDate { get; set; }
    public int UserId  { get; set; }
    public  User User { get; set; }
}