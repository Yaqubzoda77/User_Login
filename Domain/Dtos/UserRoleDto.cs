using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Dtos;

public class UserRoleDto
{
    public int UserRoleId { get; set; }
    public int UserId  { get; set; }
    public int RoleId  { get; set; }

   
    
}
