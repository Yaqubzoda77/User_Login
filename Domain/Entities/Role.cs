namespace Domain.Entities;

public class Role
{
    public int  RoleId { get; set; }
    public string Name { get; set; }
    public List<UserRole> UserRole { get; set; }
    public List<RolePremission> RolePremission { get; set; }
}