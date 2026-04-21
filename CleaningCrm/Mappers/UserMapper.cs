using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;

namespace CleaningCrm.Mappers;

public class UserMapper
{
    public UserResponse ToResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Login = user.Login,
            Role = user.Role.ToString()
        };
    }
    
}