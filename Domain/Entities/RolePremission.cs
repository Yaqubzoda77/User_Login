namespace Domain.Entities;

public class RolePremission
{
    public int  RolePremissionId { get; set; }
    public int RoleId  { get; set; }
    public  Role Role { get; set; }
    public int PremissionId  { get; set; }
    public  Premission Premission { get; set; }
}