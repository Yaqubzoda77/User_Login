namespace Domain.Entities;

public class Premission
{ 
    public int PremissionId { get; set; }
    public string Name { get; set; }
    public List<RolePremission> RolePremission { get; set; }
}